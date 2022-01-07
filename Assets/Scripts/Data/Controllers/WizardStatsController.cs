using UnityEngine;
using System.Collections.Generic;

public class WizardStatsController
{
    private WizardStatsData _wizardStatsData;
    public WizardStatsData GetWizardStatsData()
    {
        if (_wizardStatsData == null)
        {
            _wizardStatsData = LocalStorage.LoadWizardStatsData();
            if (_wizardStatsData == null)
            {
                return createNewWizard();
            }
        }
        return LocalStorage.LoadWizardStatsData();
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
    }

    private WizardStatsData createNewWizard()
    {
        WizardStatsData newWizardStats = new WizardStatsData();
        newWizardStats.StaffStatsData = new ItemStatsData();
        newWizardStats.StaffStatsData.materials = new List<string> { "blue" };
        newWizardStats.StaffStatsData.SoftMagicStats = new MagicStatsData();
        newWizardStats.StaffStatsData.SoftMagicStats.name = "Fireball";
        newWizardStats.StaffStatsData.ModerateMagicStats = new MagicStatsData();
        newWizardStats.StaffStatsData.ModerateMagicStats.name = "Fireball";
        newWizardStats.StaffStatsData.HardMagicStats = new MagicStatsData();
        newWizardStats.StaffStatsData.HardMagicStats.name = "Fireball";
        newWizardStats.CapeStatsData = new ItemStatsData();
        newWizardStats.CapeStatsData.materials = new List<string> { "blue" };
        newWizardStats.CapeStatsData.SoftMagicStats = new MagicStatsData();
        newWizardStats.CapeStatsData.SoftMagicStats.name = "MagicShieldBlue";
        newWizardStats.CapeStatsData.ModerateMagicStats = new MagicStatsData();
        newWizardStats.CapeStatsData.ModerateMagicStats.name = "MagicShieldBlue";
        newWizardStats.CapeStatsData.HardMagicStats = new MagicStatsData();
        newWizardStats.CapeStatsData.HardMagicStats.name = "MagicShieldBlue";
        newWizardStats.OrbStatsData = new ItemStatsData();
        newWizardStats.OrbStatsData.materials = new List<string> { "blue" };
        newWizardStats.OrbStatsData.SoftMagicStats = new MagicStatsData();
        newWizardStats.OrbStatsData.SoftMagicStats.name = "MagicChargeBlue";
        newWizardStats.OrbStatsData.ModerateMagicStats = new MagicStatsData();
        newWizardStats.OrbStatsData.ModerateMagicStats.name = "MagicChargeBlue";
        newWizardStats.OrbStatsData.HardMagicStats = new MagicStatsData();
        newWizardStats.OrbStatsData.HardMagicStats.name = "MagicChargeBlue";
        newWizardStats.AttackStatsData = new AttackStatsData();
        newWizardStats.DefenceStatsData = new DefenceStatsData();
        newWizardStats.ManaStatsData = new ManaStatsData();
        SaveWizardStatsData(newWizardStats);
        return newWizardStats;
    }
}