using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int currentMana;

    [SerializeField] Item _wand;
    [SerializeField] Item _cape;
    [SerializeField] Item _hat;
    public WizardStatsData WizardStatsData;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    public PlayerHUD PlayerHUD;

    void Start()
    {
        // PlayerHUD.requiredManaForSoftAttack = WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana;
        // PlayerHUD.requiredManaForModerateAttack = WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana;
        // PlayerHUD.requiredManaForHardAttack = WizardStatsData.StaffStatsData.HardMagicStats.requiredMana;
        UpdateWizard();
    }

    public void UpdateWizard()
    {
        WizardStatsData = _wizardStatsController.GetWizardStatsData();
        Debug.Log(WizardStatsData.StaffStatsData.GetMaterials()[0]);
        _wand.SetMaterials(WizardStatsData.StaffStatsData.GetMaterials());
        _wand.SetSoftMagic(WizardStatsData.StaffStatsData.SoftMagicStats.name);
        _wand.SetModerateMagic(WizardStatsData.StaffStatsData.ModerateMagicStats.name);
        _wand.SetHardMagic(WizardStatsData.StaffStatsData.HardMagicStats.name);
        _cape.SetMaterials(WizardStatsData.CapeStatsData.GetMaterials());
        _cape.SetSoftMagic(WizardStatsData.CapeStatsData.SoftMagicStats.name);
        _hat.SetMaterials(WizardStatsData.OrbStatsData.GetMaterials());
        _hat.SetSoftMagic(WizardStatsData.OrbStatsData.SoftMagicStats.name);

    }

    public float GetHealth()
    {
        return currentHealth;
    }
    public void ReduceHealth(int health)
    {
        currentHealth = (currentHealth - health);
        PlayerHUD.SetHealthBar(currentHealth);

    }

    public void IncreaseHealth(int health)
    {
        currentHealth = (currentHealth + health);
        PlayerHUD.SetHealthBar(currentHealth);
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

    public void setWand(Item wand)
    {
        this._wand = wand;
    }
    public Item getWand()
    {
        return this._wand;
    }

    public void setCape(Item cape)
    {
        this._cape = cape;
    }
    public Item getCape()
    {
        return this._cape;
    }
    public void setHat(Item hat)
    {
        this._hat = hat;
    }
    public Item getHat()
    {
        return this._hat;
    }
}
