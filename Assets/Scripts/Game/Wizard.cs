using UnityEngine;
using Photon.Pun;

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
        if (!_isDashboardWizard && _photonView && _photonView.IsMine)
        {
            _photonView.RPC("UpdateId", RpcTarget.All, Random.Range(1, 1000));
        }
    }
    [PunRPC]
    public void UpdateId(int id)
    {
        wizardId = id;
        PlayerHUD.SetWizardIndex(id);
    }
    void Start()
    {
        if (!_isDashboardWizard)
        {
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
        _anim = GetComponent<Animation>();
        WizardStatsData = _wizardStatsController.GetWizardStatsData();
        UpdateWizard(null);
    }

    private void LocateWizard()
    {
        if (wizardIndex == 1)
        {
            transform.position = new Vector3(57, 5.5f, -2f);
            wizardLocation = new Vector3(57, 5.5f, -2f);
            transform.rotation = Quaternion.Euler(-3, 0, 0);
        }
        else if (wizardIndex == 2)
        {
            transform.position = new Vector3(59, 5.5f, 2.1f);
            wizardLocation = new Vector3(59, 5.5f, 2.1f);
            transform.rotation = Quaternion.Euler(-3, 230, 0);
        }
        else
        {
            transform.position = new Vector3(54.4f, 5.5f, 2);
            wizardLocation = new Vector3(54.4f, 5.5f, 2);
            transform.rotation = Quaternion.Euler(-3, -240, 0);
        }
    }
    public void UpdateWizard(WizardStatsData wizardStatsData)
    {
        if (wizardStatsData == null)
        {
            WizardStatsData = _wizardStatsController.GetWizardStatsData();
        }
        else
        {
            WizardStatsData = wizardStatsData;
        }
        currentHealth = WizardStatsData.DefenceStatsData.MaxHP;
        if (PlayerHUD != null)
        {
            PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.DefenceStatsData.MaxHP);
            PlayerHUD.requiredManaForSoftAttack = WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana;
            PlayerHUD.requiredManaForModerateAttack = WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana;
            PlayerHUD.requiredManaForHardAttack = WizardStatsData.StaffStatsData.HardMagicStats.requiredMana;
            currentMana = WizardStatsData.ManaStatsData.StartMana;
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
            IncreaseMana(WizardStatsData.ManaStatsData.ManaRegeneration);
        }
        else if (move.wizardOption == DecisionManager.Option.Protect)
        {
            getCape().SoftMagic.Activate();
            ReduceMana(WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            IncreaseHealth(WizardStatsData.DefenceStatsData.Recovery);
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
    public void ReduceHealth(int health)
    {
        currentHealth = (currentHealth - health);
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.DefenceStatsData.MaxHP);
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
        if (newVal > WizardStatsData.DefenceStatsData.MaxHP)
        {
            currentHealth = WizardStatsData.DefenceStatsData.MaxHP;
        }
        else
        {
            currentHealth = newVal;
        }
        PlayerHUD.SetHealthBar(currentHealth, WizardStatsData.DefenceStatsData.MaxHP);
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
        if (newVal > WizardStatsData.ManaStatsData.MaxMana)
        {
            currentMana = WizardStatsData.ManaStatsData.MaxMana;
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
        ReduceHealth(1);
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

    // void ReduceHealth(bool isOpponentAttacker, bool isDefenderDefends, float attackerMagicMultiplier)
    // {
    //     if (PhotonNetwork.IsConnected)
    //     {
    //         if (PhotonNetwork.IsMasterClient)
    //         {
    //             int damagePoint = 0;
    //             if (isOpponentAttacker)
    //             {
    //                 damagePoint = CalculateDamage(_opponent.WizardStatsData, _player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
    //             }
    //             else
    //             {
    //                 damagePoint = CalculateDamage(_player.WizardStatsData, _opponent.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
    //             }
    //             if (isOpponentAttacker)
    //             {
    //                 _player.ReduceHealth(damagePoint);
    //             }
    //             else
    //             {
    //                 _opponent.ReduceHealth(damagePoint);
    //             }

    //             _photonView.RPC("ReduceHealthMulti", RpcTarget.Others, damagePoint, !isOpponentAttacker);
    //         }

    //     }
    //     else
    //     {
    //         int damagePoint = 0;
    //         if (isOpponentAttacker)
    //         {
    //             damagePoint = CalculateDamage(_opponent.WizardStatsData, _player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
    //         }
    //         else
    //         {
    //             damagePoint = CalculateDamage(_player.WizardStatsData, _opponent.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
    //         }
    //         if (isOpponentAttacker)
    //         {
    //             _player.ReduceHealth(damagePoint);
    //         }
    //         else
    //         {
    //             _opponent.ReduceHealth(damagePoint);
    //         }
    //     }
    // }

    // int CalculateDamage(WizardStatsData attacker, WizardStatsData defender, float attackerMagicMultiplier, bool isDefenderDefends)
    // {
    //     Random rnd = new Random();
    //     if (!isDefenderDefends)
    //     {
    //         if (rnd.NextDouble() <= defender.DefenceStatsData.Avoidability)
    //         {
    //             return 0;
    //         }
    //     }
    //     int damagePoints = 0;
    //     int baseDamage = rnd.Next(attacker.AttackStatsData.MinBaseDamage, attacker.AttackStatsData.MaxBaseDamage);
    //     damagePoints += baseDamage;
    //     bool isCriticalHit = rnd.NextDouble() <= attacker.AttackStatsData.CriticalRate;
    //     if (isCriticalHit)
    //     {
    //         damagePoints += (int)((attacker.AttackStatsData.CriticalDmg + attackerMagicMultiplier) * baseDamage);
    //     }
    //     else
    //     {
    //         damagePoints += (int)((attackerMagicMultiplier) * baseDamage);
    //     }
    //     if (isDefenderDefends)
    //     {
    //         damagePoints = (int)(attackerMagicMultiplier * damagePoints);
    //     }
    //     return damagePoints;
    // }
    // [PunRPC]
    // void SyncOpponentPlayer(string wizardStatsDataRaw)
    // {
    //     WizardStatsData wizardStatsData = JsonUtility.FromJson<WizardStatsData>(wizardStatsDataRaw);
    //     _opponent.UpdateWizard(wizardStatsData);
    // }

    // [PunRPC]
    // void ReduceHealthMulti(int damagePoint, bool isOpponent)
    // {
    //     if (isOpponent)
    //     {
    //         _player.ReduceHealth(damagePoint);
    //     }
    //     else
    //     {
    //         _opponent.ReduceHealth(damagePoint);
    //     }

    // }

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