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
        newWizardStats.StaffStatsData = new StaffStatsData();
        newWizardStats.StaffStatsData.materials = new List<string> { "blue" };
        newWizardStats.StaffStatsData.SoftMagicStats = new AttackMagicStatsData();
        newWizardStats.StaffStatsData.SoftMagicStats.name = "Fireball";
        newWizardStats.StaffStatsData.ModerateMagicStats = new AttackMagicStatsData();
        newWizardStats.StaffStatsData.ModerateMagicStats.name = "Fireball";
        newWizardStats.StaffStatsData.HardMagicStats = new AttackMagicStatsData();
        newWizardStats.StaffStatsData.HardMagicStats.name = "Fireball";
        newWizardStats.CapeStatsData = new CapeStatsData();
        newWizardStats.CapeStatsData.materials = new List<string> { "blue" };
        newWizardStats.CapeStatsData.SoftMagicStats = new DefenceMagicStatsData();
        newWizardStats.CapeStatsData.SoftMagicStats.name = "Healing";
        newWizardStats.CapeStatsData.ModerateMagicStats = new DefenceMagicStatsData();
        newWizardStats.CapeStatsData.ModerateMagicStats.name = "Healing";
        newWizardStats.CapeStatsData.HardMagicStats = new DefenceMagicStatsData();
        newWizardStats.CapeStatsData.HardMagicStats.name = "Healing";
        newWizardStats.OrbStatsData = new OrbStatsData();
        newWizardStats.OrbStatsData.materials = new List<string> { "blue" };
        newWizardStats.OrbStatsData.SoftMagicStats = new ManaMagicStatsData();
        newWizardStats.OrbStatsData.SoftMagicStats.name = "Magic aura";
        newWizardStats.OrbStatsData.ModerateMagicStats = new ManaMagicStatsData();
        newWizardStats.OrbStatsData.ModerateMagicStats.name = "Magic aura";
        newWizardStats.OrbStatsData.HardMagicStats = new ManaMagicStatsData();
        newWizardStats.OrbStatsData.HardMagicStats.name = "Magic aura";
        newWizardStats.AttackStatsData = new AttackStatsData();
        newWizardStats.DefenceStatsData = new DefenceStatsData();
        newWizardStats.ManaStatsData = new ManaStatsData();
        SaveWizardStatsData(newWizardStats);
        return newWizardStats;
    }
}