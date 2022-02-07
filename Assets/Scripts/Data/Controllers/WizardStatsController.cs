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
        newWizardStats.BaseAttackStatsData = new AttackStatsData();
        newWizardStats.BaseDefenceStatsData = new DefenceStatsData();
        newWizardStats.BaseManaStatsData = new ManaStatsData();
        return newWizardStats;
    }
}