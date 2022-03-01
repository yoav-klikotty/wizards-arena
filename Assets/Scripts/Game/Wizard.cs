﻿using UnityEngine;
using Photon.Pun;
using Random = System.Random;
using System.Collections.Generic;
using System;

public class Wizard : MonoBehaviour
{

    PhotonView _photonView;
    public const string IDLE = "Wizard_Idle";
    public const string RUN = "Wizard_Run";
    public const string ATTACK = "Wizard_Attack";
    public const string SKILL = "Wizard_Skill";
    public const string DAMAGE = "Wizard_Damage";
    public const string STUN = "Wizard_Stun";
    public const string DEATH = "Wizard_Death";
    Animation _anim;
    [SerializeField] int _currentHealth;
    [SerializeField] int _currentMana;
    public DateTime DeathTime;
    [SerializeField] Item _staff;
    [SerializeField] Item _cape;
    [SerializeField] Item _orb;
    [SerializeField] bool _isDashboardWizard;
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    [SerializeField] GameObject _shootCenter;
    public WizardStatsData WizardStatsData;
    WizardStatsController _wizardStatsController = new WizardStatsController();
    public PlayerHUD PlayerHUD;
    SessionManager _sessionManager;
    public int wizardIndex;
    public int wizardId;
    Vector3 wizardLocation = new Vector3(59.5f, 4.4f, 2.1f);
    public Dictionary<string, Magic> magics = new Dictionary<string, Magic>();
    void Awake()
    {
        wizardIndex = GameObject.FindGameObjectsWithTag("Player").Length;
        _photonView = PhotonView.Get(this);
        WizardStatsData = _wizardStatsController.GetWizardStatsData();
        if (!_isDashboardWizard)
        {
            if (_photonView && _photonView.IsMine)
            {
                WizardStatsData = _wizardStatsController.GetWizardStatsData();
                string WizardStatsDataRaw = JsonUtility.ToJson(WizardStatsData);
                _photonView.RPC("UpdateWizardStats", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, WizardStatsDataRaw);
            }
        }
    }

    [PunRPC]
    public void UpdateWizardStats(int id, string wizardStatsRaw)
    {
        wizardId = id;
        UpdateWizard(JsonUtility.FromJson<WizardStatsData>(wizardStatsRaw));
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        LocateWizard();
        _sessionManager.RegisterWizard(this);
        if (_photonView.IsMine)
        {
            _sessionManager.playerWizard = this;
            this.gameObject.name = "Player";
        }
        else
        {
            this.gameObject.name = "Opponent";
        }
    }
    void Start()
    {
        if (_isDashboardWizard)
        {
            UpdateWizard(_wizardStatsController.GetWizardStatsData());
        }
        _anim = GetComponent<Animation>();
    }

    private void LocateWizard()
    {
        if (wizardIndex == 1)
        {
            transform.position = new Vector3(60, 5.5f, -5f);
            wizardLocation = new Vector3(60, 5.5f, -5f);
            transform.rotation = Quaternion.Euler(-3, -45, 0);
        }
        else if (wizardIndex == 2)
        {
            transform.position = new Vector3(54, 5.5f, 1f);
            wizardLocation = new Vector3(54, 5.5f, 1f);
            transform.rotation = Quaternion.Euler(-3, 120, 0);
        }
        else if (wizardIndex == 3)
        {
            transform.position = new Vector3(60f, 5.5f, 1);
            wizardLocation = new Vector3(60f, 5.5f, 1);
            transform.rotation = Quaternion.Euler(-3, -130, 0);
        }
        else
        {
            transform.position = new Vector3(54f, 5.5f, -5);
            wizardLocation = new Vector3(54f, 5.5f, -5);
            transform.rotation = Quaternion.Euler(-3, 60, 0);
        }
    }
    public void UpdateWizard(WizardStatsData wizardStatsData)
    {
        WizardStatsData = wizardStatsData;
        _currentHealth = WizardStatsData.GetTotalHP();
        foreach (MagicStatsData magicStats in wizardStatsData.MagicsStatsData)
        {
            var magic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + magicStats.name), transform.position, Quaternion.identity);
            magic.transform.parent = gameObject.transform;
            if (!magics.ContainsKey(magicStats.name))
            {
                magics.Add(magicStats.name, magic.GetComponent<Magic>());
            }
        }
        if (PlayerHUD != null)
        {
            PlayerHUD.UpdateHealth(_currentHealth, WizardStatsData.GetTotalHP());
            _currentMana = WizardStatsData.GetTotalStartMana();
            PlayerHUD.UpdateMana(_currentMana, WizardStatsData.GetTotalMaxMana());
        }
        _staff.SetMaterials(WizardStatsData.StaffStatsData.GetMaterials());
        _cape.SetMaterials(WizardStatsData.CapeStatsData.GetMaterials());
        _orb.SetMaterials(WizardStatsData.OrbStatsData.GetMaterials());
    }

    public void RenderDecision(WizardMove move)
    {
        if (move.wizardOption != "game over")
        {
            Magic magic = magics[move.wizardOption];
            if (magic.GetMagicType() == Magic.MagicType.Attack)
            {
                magic.ActivateFirePrefab(_shootCenter.transform.position, move.wizardOpponentPosition);
                AttackAni();
                ReduceMana(magic.GetRequiredMana());
            }
            else if (magic.GetMagicType() == Magic.MagicType.Mana)
            {
                magic.Activate();
                IncreaseMana(WizardStatsData.GetTotalManaRegeneration());
            }
            else
            {
                magic.Activate();
                ReduceMana(magic.GetRequiredMana());
                IncreaseHealth(WizardStatsData.GetTotalRecovery());
            }
        }

    }
    public void ChooseMove(string option, int id)
    {
        if (_photonView.IsMine)
        {
            _photonView.RPC("ChooseMoveRPC", RpcTarget.All, option, id);
        }
    }
    [PunRPC]
    public void ChooseMoveRPC(string option, int id)
    {
        WizardMove move = new WizardMove(option, _sessionManager.GetWizardById(id).wizardLocation);
        _sessionManager.RegisterWizardMove(wizardIndex - 1, move);
    }
    public float GetHealth()
    {
        return _currentHealth;
    }

    [PunRPC]
    public void ReduceHealth(int health, bool isCrit)
    {
        _currentHealth = (_currentHealth - health);
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        PlayerHUD.UpdateHealth(_currentHealth, WizardStatsData.GetTotalHP());
        if (health == 0)
        {
            PlayerHUD.ActivateIndication("Avoided!", "avoid");
        }
        else
        {
            string hit = isCrit ? "crit" : "hit";
            PlayerHUD.ActivateIndication("" + health, hit);
        }

    }

    public void IncreaseHealth(int health)
    {
        int newVal = (_currentHealth + health);
        if (newVal > WizardStatsData.GetTotalHP())
        {
            _currentHealth = WizardStatsData.GetTotalHP();
        }
        else
        {
            _currentHealth = newVal;
        }
        PlayerHUD.UpdateHealth(_currentHealth, WizardStatsData.GetTotalHP());
        if (health > 0) PlayerHUD.ActivateIndication("" + health, "heal");
    }

    public int GetMana()
    {
        return _currentMana;
    }
    public void ReduceMana(int mana)
    {
        int newVal = _currentMana - mana;
        if (newVal < 0)
        {
            _currentMana = 0;
        }
        else
        {
            _currentMana = newVal;
        }
        PlayerHUD.UpdateMana(_currentMana, WizardStatsData.GetTotalMaxMana());
    }

    public void IncreaseMana(int mana)
    {
        int newVal = _currentMana + mana;
        if (newVal > WizardStatsData.GetTotalMaxMana())
        {
            _currentMana = WizardStatsData.GetTotalMaxMana();
        }
        else
        {
            _currentMana = newVal;
        }
        if (mana > 0) {
            PlayerHUD.ActivateIndication("" + mana, "mana");
            PlayerHUD.UpdateMana(_currentMana, WizardStatsData.GetTotalMaxMana());
        }
    }

    public void IdleAni()
    {
        _anim.CrossFade(IDLE);
    }

    public void RunAni()
    {
        _anim.CrossFade(RUN);
    }

    public void AttackAni()
    {
        _anim.CrossFade(ATTACK);
    }

    public void SkillAni()
    {
        _anim.CrossFade(SKILL);
    }

    public void DamageAni()
    {
        _anim.CrossFade(DAMAGE);
    }

    public void StunAni()
    {
        _anim.CrossFade(STUN);
    }

    public void DeathAni()
    {
        _anim.CrossFade(DEATH);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (_photonView && _photonView.IsMine)
        {
            Random rnd = new Random();
            var attacker = collision.gameObject.GetComponent<ProjectileMover>().wizardStats;
            int damage = CalculateBaseDamage(attacker);
            bool isCriticalHit = rnd.NextDouble() <= attacker.GetTotalCriticalRate();
            if (isCriticalHit)
            {
                damage += CalculateCritDamage(damage, attacker);
            }
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage, isCriticalHit);
        }
        DamageAni();
        if (GetHealth() <= 0)
        {
            DeathTime = DateTime.Now;
            DeathAni();
        }
    }
    public bool IsWizardAlive()
    {
        return DeathTime == DateTime.MinValue;
    }
    public void StopAni()
    {
        _anim.Stop();
    }

    public void ResetLocation()
    {
        EnableWizard();
        Vector3 newLocation = new Vector3(-0.9f, -0.1f, 0);
        transform.position = newLocation;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    public void TiltLocation()
    {
        EnableWizard();
        Vector3 newLocation = new Vector3(-0.9f, -0.1f, 0);
        transform.position = newLocation;
        transform.localRotation = Quaternion.Euler(20, 150, -10);
        IdleAni();
    }
    public void DisabledWizard()
    {
        gameObject.SetActive(false);
    }

    public void EnableWizard()
    {
        gameObject.SetActive(true);
    }

    public void setStaff(Item staff)
    {
        this._staff = staff;
    }
    public Item getStaff()
    {
        return this._staff;
    }

    public void setCape(Item cape)
    {
        this._cape = cape;
    }
    public Item getCape()
    {
        return this._cape;
    }
    public void setOrb(Item orb)
    {
        this._orb = orb;
    }
    public Item getOrb()
    {
        return this._orb;
    }
    int CalculateBaseDamage(WizardStatsData attacker)
    {
        Random rnd = new Random();
        int damagePoints = 0;
        int baseDamage = rnd.Next(attacker.GetTotalBaseDamage(), attacker.GetTotalBaseDamage());
        damagePoints += baseDamage;
        return damagePoints;
    }
    int CalculateCritDamage(int damage, WizardStatsData attacker)
    {
        return (int)((attacker.GetTotalCriticalDmg()) * damage);
    }
    public void IsWizardSelected(bool isOn)
    {
        TurnWizardSelection(true);
        if (isOn)
        {
            m_SpriteRenderer.color = Color.green;
        }
        else
        {
            m_SpriteRenderer.color = Color.red;
        }
    }

    public void TurnWizardSelection(bool isOn)
    {
        if (isOn)
        {
            m_SpriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            m_SpriteRenderer.gameObject.SetActive(false);
        }
    }


    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
    // _opponent = GameObject.Find("Opponent").GetComponent<Wizard>();
    // _player = GameObject.Find("Player").GetComponent<Wizard>();
    // _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
    // _photonView = PhotonView.Get(this);
    // if (PhotonNetwork.IsConnected)
    // {
    //     WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
    //     _photonView.RPC("SyncOpponentPlayer", RpcTarget.Others, JsonUtility.ToJson(wizardStatsData));
    // }
    // else
    // {
    //     WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
    //     _opponent.UpdateWizard(wizardStatsData);
    // }


}