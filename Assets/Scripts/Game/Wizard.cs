using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{

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
    public WizardStatsData WizardStatsData;
    WizardStatsController _wizardStatsController = new WizardStatsController();
    public PlayerHUD PlayerHUD;
    void Start()
    {
        _anim = GetComponent<Animation>();
        WizardStatsData = _wizardStatsController.GetWizardStatsData();
        UpdateWizard(null);
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

}