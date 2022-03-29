using UnityEngine;
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
            var magic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + magicStats.type.ToString() + "/" + magicStats.name), transform.position, Quaternion.identity, gameObject.transform);
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
                IncreaseMana(WizardStatsData.GetTotalManaRegeneration() + magic.ManaStatsData.ManaRegeneration);
            }
            else
            {
                magic.Activate();
                ReduceMana(magic.GetRequiredMana());
                IncreaseHealth(WizardStatsData.GetTotalRecovery() + magic.DefenceStatsData.Recovery);
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
    public void ReduceHealth(int health, bool isCrit, bool isAvoided)
    {
        if (isAvoided)
        {
            PlayerHUD.ActivateIndication("Avoided!", indicationEvents.avoid);
        }
        else if(health > 0)
        {
            _currentHealth = (_currentHealth - health);
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                Debug.Log("death");
                DeathTime = DateTime.Now;
                DeathAni();
            }
            else {
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
        IdleAni();
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
        if (_photonView && _photonView.IsMine)
        {
            var attacker = collision.gameObject.GetComponent<ProjectileMover>().wizardStats;
            var attackerMagic = collision.gameObject.GetComponent<ProjectileMover>().AttackStatsData;
            Damage damage = CalculateDamage(attacker, attackerMagic);
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage.damage, damage.criticalHit, damage.avoided);
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
    public Damage CalculateDamage(WizardStatsData attacker, AttackStatsData attackerMagic)
    {
        Random rnd = new Random();
        Damage damage = new Damage();
        
        double randomFactor = rnd.NextDouble();

        if(randomFactor <= WizardStatsData.GetTotalAvoidability())
        {
            damage.avoided = true;
        }

        damage.damage = attacker.GetTotalBaseDamage() + attackerMagic.BaseDamage;
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
        if (isOn)
        {
            m_SpriteRenderer.gameObject.SetActive(true);
        }
        else
        {
            m_SpriteRenderer.gameObject.SetActive(false);
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

    public void OnShieldCollision(WizardStatsData attacker, AttackStatsData attackerMagic, int shieldHP)
    {
        if (_photonView && _photonView.IsMine)
        {
            Damage damage = CalculateDamage(attacker, attackerMagic);
            int shieldDmg = 0;
            if (damage.damage > shieldHP)
            {
                damage.damage = (damage.damage - shieldHP) + ((int)(shieldHP * (attacker.GetTotalArmorPenetration() + attackerMagic.ArmorPenetration)));
                shieldDmg = shieldHP;
            }
            else
            {
                shieldDmg = damage.damage;
                damage.damage = (int)(damage.damage * (attacker.GetTotalArmorPenetration() + attackerMagic.ArmorPenetration));
            }
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage.damage, damage.criticalHit, damage.avoided);
            _photonView.RPC("ReduceShield", RpcTarget.All, shieldDmg, damage.criticalHit);
        }
    }
}