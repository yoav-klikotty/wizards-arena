﻿using UnityEngine;
using Photon.Pun;
using Random = System.Random;
using System.Collections.Generic;
using System;
using System.Collections;

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
    [SerializeField] WizardSoundManager _wizardSoundManager;
    [SerializeField] int _currentHealth;
    [SerializeField] int _currentMana;
    public DateTime DeathTime;
    [SerializeField] Item _staff;
    [SerializeField] Item _cape;
    [SerializeField] Item _orb;
    [SerializeField] bool _isDashboardWizard;
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    [SerializeField] GameObject[] _shootCenter;
    public WizardStatsData WizardStatsData;
    public PlayerStatsData PlayerStatsData;
    public PlayerHUD PlayerHUD;
    SessionManager _sessionManager;
    public int wizardIndex;
    public string wizardId;
    public Vector3 wizardLocation = new Vector3(59.5f, 4.4f, 2.1f);
    public WizardMove move = new WizardMove(null, Vector3.zero);
    public Dictionary<string, Magic> magics = new Dictionary<string, Magic>();
    public bool isBot;
    public int Hits;

    void Awake()
    {
        PlayerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        wizardIndex = GameObject.FindGameObjectsWithTag("Player").Length;
        WizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
        string WizardStatsDataRaw = JsonUtility.ToJson(WizardStatsData);
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        if (!GameManager.Instance.Offline)
        {
            _photonView = PhotonView.Get(this);
            if (_photonView && _photonView.IsMine)
            {
                _photonView.RPC("UpdateWizardStats", RpcTarget.All, isBot ? PhotonNetwork.LocalPlayer.UserId + wizardIndex : PhotonNetwork.LocalPlayer.UserId, WizardStatsDataRaw);
            }
        }
        else
        {
            UpdateWizardStats(wizardIndex + "", WizardStatsDataRaw);
        }
    }

    [PunRPC]
    public void UpdateWizardStats(string id, string wizardStatsRaw)
    {
        wizardId = id;
        UpdateWizard(JsonUtility.FromJson<WizardStatsData>(wizardStatsRaw));
        LocateWizard();
        _sessionManager.RegisterWizard(this);
        if (GameManager.Instance.Offline)
        {
            if (id == "1")
            {
                _sessionManager.playerWizard = this;
                this.gameObject.name = "Player";
            }
            else
            {
                this.gameObject.name = "Opponent";

            }
            return;
        }
        if (_photonView.IsMine && !isBot)
        {
            _sessionManager.playerWizard = this;
            this.gameObject.name = "Player";
        }
        else
        {
            this.gameObject.name = "Opponent";
        }
        if (isBot)
        {
            CreateBotMove();
        }
    }
    void Start()
    {
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
            transform.position = new Vector3(56f, 5.5f, -5.5f);
            wizardLocation = new Vector3(56f, 5.5f, -5.5f);
            transform.rotation = Quaternion.Euler(-3, 50, 0);
        }
    }
    public void UpdateWizard(WizardStatsData wizardStatsData)
    {
        WizardStatsData = wizardStatsData;
        _currentHealth = WizardStatsData.GetTotalHP();
        foreach (MagicStatsData magicStats in wizardStatsData.MagicsStatsData)
        {
            var magicPrefab = Resources.Load("Prefabs/" + "Magics/" + magicStats.type.ToString() + "/" + magicStats.name);
            var magic = (GameObject)Instantiate(magicPrefab, transform.position, Quaternion.identity, gameObject.transform);
            if (!magics.ContainsKey(magicStats.name))
            {
                magics.Add(magicStats.name, magic.GetComponent<Magic>());
            }
        }
        if (PlayerHUD != null)
        {
            PlayerHUD.UpdateHealth(_currentHealth, WizardStatsData.GetTotalHP());
            PlayerHUD.UpdateHits(0);
            _currentMana = WizardStatsData.GetTotalStartMana();
            PlayerHUD.UpdateMana(_currentMana, WizardStatsData.GetTotalMaxMana());
        }
        _staff.SetMaterials(WizardStatsData.StaffStatsData.GetMaterials());
        _cape.SetMaterials(WizardStatsData.CapeStatsData.GetMaterials());
        _orb.SetMaterials(WizardStatsData.OrbStatsData.GetMaterials());
    }
    public void RenderDecision()
    {
        if (move.wizardOption != "game over")
        {
            Magic magic = magics[move.wizardOption];
            if (magic.GetMagicType() == Magic.MagicType.Attack)
            {
                if (magic.AOE)
                {
                    for (int i = 0; i < _sessionManager.wizards.Count; i++)
                    {
                        if (wizardId != _sessionManager.wizards[i].wizardId)
                        {
                            magic.ActivateFirePrefab(_shootCenter[i].transform.position, _sessionManager.wizards[i].wizardLocation);
                        }
                    }
                }
                else
                {
                    magic.ActivateFirePrefab(_shootCenter[0].transform.position, move.wizardOpponentPosition);
                }
                AttackAni();
                if ((magic.GetRequiredHp() > 0))
                {
                    ReduceHealth((magic.GetRequiredHp()), false, false, wizardId);
                }
                ReduceMana(magic.GetRequiredMana());
            }
            else if (magic.GetMagicType() == Magic.MagicType.Mana)
            {
                magic.Activate();
                if ((magic.GetRequiredHp() > 0))
                {
                    ReduceHealth((magic.GetRequiredHp()), false, false, wizardId);
                }
                IncreaseMana(WizardStatsData.GetTotalManaRegeneration() + magic.ManaStatsData.ManaRegeneration);
            }
            else
            {
                magic.Activate();
                ReduceMana(magic.GetRequiredMana());
                if ((magic.GetRequiredHp() > 0))
                {
                    ReduceHealth((magic.GetRequiredHp()), false, false, wizardId);
                }
                IncreaseHealth(WizardStatsData.GetTotalRecovery() + magic.DefenceStatsData.Recovery);
            }
            if (isBot)
            {
                Invoke("CreateBotMove", 3);
            }
            _wizardSoundManager.PlayActionsSound(magic.getSound());
            move = new WizardMove(null, Vector3.zero);
            TurnWizardSelection(false);
        }

    }
    public void ChooseMove(string option, string id)
    {
        if (GameManager.Instance.Offline)
        {
            ChooseMoveRPC(option, id);
            return;
        }
        if (_photonView.IsMine)
        {
            _photonView.RPC("ChooseMoveRPC", RpcTarget.All, option, id);
        }
    }
    [PunRPC]
    public void ChooseMoveRPC(string option, string id)
    {
        move = new WizardMove(option, _sessionManager.GetWizardById(id).wizardLocation);
        RenderDecision();
    }
    public float GetHealth()
    {
        return _currentHealth;
    }

    [PunRPC]
    public void ReduceHealth(int health, bool isCrit, bool isAvoided, string wizradId)
    {
        if (isAvoided)
        {
            PlayerHUD.ActivateIndication("Avoided!", indicationEvents.avoid);
        }
        else if (health > 0)
        {
            _currentHealth = (_currentHealth - health);
            if (_currentHealth <= 0)
            {
                _sessionManager.UpdateWizardHits(wizradId);
                _currentHealth = 0;
                DeathTime = DateTime.Now;
                DeathAni();
            }
            else
            {
                DamageAni();
            }
            PlayerHUD.UpdateHealth(_currentHealth, WizardStatsData.GetTotalHP());
            indicationEvents hitType = isCrit ? indicationEvents.crit : indicationEvents.hit;
            PlayerHUD.ActivateIndication("" + health, hitType);
        }
    }

    [PunRPC]
    public void ReduceShield(int dmg)
    {
        StartCoroutine(DelayIndicator(dmg, 1f));
    }
    IEnumerator DelayIndicator(int dmg, float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        PlayerHUD.ActivateIndication("" + dmg, indicationEvents.shield);
    }
    public void IncreaseHits()
    {
        Hits += 1;
        PlayerHUD.UpdateHits(Hits);
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
        if (health > 0) PlayerHUD.ActivateIndication("" + health, indicationEvents.heal);
    }
    public int GetMana()
    {
        return _currentMana;
    }
    [PunRPC]
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
        if (mana > 0)
        {
            PlayerHUD.ActivateIndication("" + mana, indicationEvents.mana);
            PlayerHUD.UpdateMana(_currentMana, WizardStatsData.GetTotalMaxMana());
        }
    }

    IEnumerator ResetAnim(float animationDuration)
    {
        yield return new WaitForSeconds(animationDuration);
        if (IsWizardAlive())
        {
            IdleAni();
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
        StartCoroutine(ResetAnim(1f));
    }

    public void SkillAni()
    {
        _anim.CrossFade(SKILL);
    }

    public void DamageAni()
    {
        _anim.CrossFade(DAMAGE);
        StartCoroutine(ResetAnim(0.66f));
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
        _wizardSoundManager.PlayWizardHitSound();
        if (_photonView && _photonView.IsMine)
        {
            var attacker = collision.gameObject.GetComponent<ProjectileMover>().Attacker;
            var attackerMagic = collision.gameObject.GetComponent<ProjectileMover>().MagicAttackStatsData;
            var attackerMagicSpecialEffects = collision.gameObject.GetComponent<ProjectileMover>().MagicAttackSpecialEffects;
            Damage damage = CalculateDamage(attacker.WizardStatsData, attackerMagic);
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage.damage, damage.criticalHit, damage.avoided, attacker.wizardId);
            if (attackerMagicSpecialEffects.ManaBurn > 0)
            {
                _photonView.RPC("ReduceMana", RpcTarget.All, attackerMagicSpecialEffects.ManaBurn);
            }
        }
    }
    public bool IsWizardAlive()
    {
        return DeathTime == DateTime.MinValue;
    }
    public Damage CalculateDamage(WizardStatsData attacker, AttackStatsData attackerMagic)
    {
        Random rnd = new Random();
        Damage damage = new Damage();

        double randomFactor = rnd.NextDouble();

        if (randomFactor <= WizardStatsData.GetTotalAvoidability())
        {
            damage.avoided = true;
        }
        if (attackerMagic.ScaledValue)
        {
            damage.damage = (int)(attacker.GetTotalBaseDamage() * (float)attackerMagic.BaseDamage / 100);
        }
        else
        {
            damage.damage = attacker.GetTotalBaseDamage() + attackerMagic.BaseDamage;
        }
        if (randomFactor <= (attacker.GetTotalCriticalRate() + attackerMagic.CriticalRate))
        {
            damage.criticalHit = true;
            damage.damage += (int)((attacker.GetTotalCriticalDmg() + attackerMagic.CriticalDmg) * damage.damage);
        }
        return damage;
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
        if (IsWizardAlive())
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

    }

    public class Damage
    {
        public Damage()
        {
            damage = 0;
            criticalHit = false;
            avoided = false;
        }
        public int damage;
        public bool criticalHit;
        public bool avoided;
    }

    public void OnShieldCollision(Wizard attacker, AttackStatsData attackerMagic, int shieldHP)
    {
        _wizardSoundManager.PlayShieldHitSound();
        if (_photonView && _photonView.IsMine)
        {
            Damage damage = CalculateDamage(attacker.WizardStatsData, attackerMagic);
            int shieldDmg = 0;
            if (damage.damage > shieldHP)
            {
                damage.damage = (damage.damage - shieldHP) + ((int)(shieldHP * (attacker.WizardStatsData.GetTotalArmorPenetration() + attackerMagic.ArmorPenetration)));
                shieldDmg = shieldHP;
            }
            else
            {
                shieldDmg = damage.damage;
                damage.damage = (int)(damage.damage * (attacker.WizardStatsData.GetTotalArmorPenetration() + attackerMagic.ArmorPenetration));
            }
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage.damage, damage.criticalHit, damage.avoided, attacker.wizardId);
            _photonView.RPC("ReduceShield", RpcTarget.All, shieldDmg);
        }
    }
    private void CreateBotMove()
    {
        var opponentId = _sessionManager.GetRandomOpponentId(wizardId).wizardId;
        if (!IsWizardAlive())
        {
            ChooseMove("game over", opponentId);
            return;
        }
        var attackMagic = "";
        var manaMagic = "";
        var defenceMagic = "";
        foreach (MagicStatsData magicStats in WizardStatsData.MagicsStatsData)
        {
            if (magicStats.type == Magic.MagicType.Mana)
            {
                manaMagic = magicStats.name;
            }
            else if (magicStats.type == Magic.MagicType.Defence)
            {
                if (magics[magicStats.name].GetRequiredMana() < _currentMana && magics[magicStats.name].GetRequiredHp() < _currentHealth)
                {
                    defenceMagic = magicStats.name;
                }
            }
            else
            {
                if (magics[magicStats.name].GetRequiredMana() < _currentMana && magics[magicStats.name].GetRequiredHp() < _currentHealth)
                {
                    attackMagic = magicStats.name;
                }
            }
        }
        var options = new List<string>();
        if (!attackMagic.Equals(""))
        {
            options.Add(attackMagic);
        }
        if (!manaMagic.Equals(""))
        {
            options.Add(manaMagic);
        }
        if (!defenceMagic.Equals(""))
        {
            options.Add(defenceMagic);
        }
        Random rnd = new Random();
        ChooseMove(options[rnd.Next(0, options.Count)], opponentId);
    }
}