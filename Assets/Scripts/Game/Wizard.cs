using UnityEngine;
using Photon.Pun;
using Random = System.Random;

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
    [SerializeField] int currentHealth;
    [SerializeField] int currentMana;
    [SerializeField] Item _staff;
    [SerializeField] Item _cape;
    [SerializeField] Item _orb;
    [SerializeField] bool _isDashboardWizard;
    GameObject attackedWizard;
    [SerializeField] GameObject _shootCenter;
    public WizardStatsData WizardStatsData;
    WizardStatsController _wizardStatsController = new WizardStatsController();
    public PlayerHUD PlayerHUD;
    SessionManager _sessionManager;
    public int wizardIndex;
    public int wizardId;
    Vector3 wizardLocation = new Vector3(59.5f, 4.4f, 2.1f);
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
        PlayerHUD.SetWizardIndex(id);
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
        currentHealth = WizardStatsData.GetTotalHP();
        if (PlayerHUD != null)
        {
            PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.GetTotalHP());
            PlayerHUD.requiredManaForSoftAttack = WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana;
            PlayerHUD.requiredManaForModerateAttack = WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana;
            PlayerHUD.requiredManaForHardAttack = WizardStatsData.StaffStatsData.HardMagicStats.requiredMana;
            currentMana = WizardStatsData.GetTotalStartMana();
            PlayerHUD.RenderAvailableAttacks(currentMana);
        }
        _staff.SetMaterials(WizardStatsData.StaffStatsData.GetMaterials());
        _staff.SetMagics(
            WizardStatsData.StaffStatsData.SoftMagicStats.name,
            WizardStatsData.StaffStatsData.ModerateMagicStats.name,
            WizardStatsData.StaffStatsData.HardMagicStats.name
        );
        _cape.SetMaterials(WizardStatsData.CapeStatsData.GetMaterials());
        _cape.SetMagics(
            WizardStatsData.CapeStatsData.SoftMagicStats.name,
            WizardStatsData.CapeStatsData.ModerateMagicStats.name,
            WizardStatsData.CapeStatsData.HardMagicStats.name
        );
        _orb.SetMaterials(WizardStatsData.OrbStatsData.GetMaterials());
        _orb.SetMagics(
            WizardStatsData.OrbStatsData.SoftMagicStats.name,
            WizardStatsData.OrbStatsData.ModerateMagicStats.name,
            WizardStatsData.OrbStatsData.HardMagicStats.name
        );
    }

    public void RenderDecision(WizardMove move)
    {
        if (move.wizardOption == DecisionManager.Option.Reload)
        {
            getOrb().SoftMagic.Activate();
            IncreaseMana(WizardStatsData.GetTotalManaRegeneration());
        }
        else if (move.wizardOption == DecisionManager.Option.Protect)
        {
            getCape().SoftMagic.Activate();
            ReduceMana(WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            IncreaseHealth(WizardStatsData.GetTotalRecovery());
        }
        else if (move.wizardOption == DecisionManager.Option.SoftAttack)
        {
            getStaff().SoftMagic.ActivateFirePrefab(_shootCenter.transform.position, move.wizardOpponentPosition);
            AttackAni();
            ReduceMana(WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (move.wizardOption == DecisionManager.Option.ModerateAttack)
        {
            getStaff().ModerateMagic.ActivateFirePrefab(_shootCenter.transform.position, move.wizardOpponentPosition);
            AttackAni();
            ReduceMana(WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (move.wizardOption == DecisionManager.Option.HardAttack)
        {
            getStaff().HardMagic.ActivateFirePrefab(_shootCenter.transform.position, move.wizardOpponentPosition);
            AttackAni();
            ReduceMana(WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

    }
    public void ChooseMove(DecisionManager.Option option, int id)
    {
        if (_photonView.IsMine)
        {
            _photonView.RPC("ChooseMoveRPC", RpcTarget.All, option, id);
        }
    }
    [PunRPC]
    public void ChooseMoveRPC(DecisionManager.Option option, int id)
    {
        WizardMove move = new WizardMove(option, _sessionManager.GetWizardById(id).wizardLocation);
        _sessionManager.RegisterWizardMove(wizardIndex - 1, move);
    }
    public float GetHealth()
    {
        return currentHealth;
    }

    [PunRPC]
    public void ReduceHealth(int health)
    {
        currentHealth = (currentHealth - health);
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.GetTotalHP());
        if (health == 0)
        {
            PlayerHUD.ActivateIndication("Strike Avoid!");

        }
        else
        {
            PlayerHUD.ActivateIndication("- " + health + " HP");
        }

    }

    public void IncreaseHealth(int health)
    {
        int newVal = (currentHealth + health);
        if (newVal > WizardStatsData.GetTotalHP())
        {
            currentHealth = WizardStatsData.GetTotalHP();
        }
        else
        {
            currentHealth = newVal;
        }
        PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.GetTotalHP());
    }

    public int GetMana()
    {
        return currentMana;
    }
    public void ReduceMana(int mana)
    {
        int newVal = currentMana - mana;
        if (newVal < 0)
        {
            currentMana = 0;
        }
        else
        {
            currentMana = newVal;
        }
        PlayerHUD.RenderAvailableAttacks(currentMana);
    }

    public void IncreaseMana(int mana)
    {
        int newVal = currentMana + mana;
        if (newVal > WizardStatsData.GetTotalMaxMana())
        {
            currentMana = WizardStatsData.GetTotalMaxMana();
        }
        else
        {
            currentMana = newVal;
        }
        PlayerHUD.RenderAvailableAttacks(currentMana);
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
            int damage = CalculateDamage(collision.gameObject.GetComponent<ProjectileMover>().wizardStats, WizardStatsData);
            Debug.Log(collision.gameObject);
            _photonView.RPC("ReduceHealth", RpcTarget.All, damage);
        }
        DamageAni();
        if (GetHealth() <= 0)
        {
            DeathAni();
        }
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
    int CalculateDamage(WizardStatsData attacker, WizardStatsData defender)
    {
        Random rnd = new Random();
        int damagePoints = 0;
        int baseDamage = rnd.Next(attacker.GetTotalBaseDamage(), attacker.GetTotalBaseDamage());
        damagePoints += baseDamage;
        bool isCriticalHit = rnd.NextDouble() <= attacker.GetTotalCriticalRate();
        if (isCriticalHit)
        {
            damagePoints += (int)((attacker.GetTotalCriticalDmg()) * baseDamage);
        }
        return damagePoints;
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