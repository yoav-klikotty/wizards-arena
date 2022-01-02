using UnityEngine;

public class WizardStatsController
{
    private WizardStatsData _wizardStatsData; 
    public WizardStatsData GetWizardStatsData()
    {
        if (_wizardStatsData == null)
        {
            _wizardStatsData = LocalStorage.LoadWizardStatsData();
            if(_wizardStatsData == null) {
                return createNewWizard();
            }
        }
        return _wizardStatsData;
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
    }

    private WizardStatsData createNewWizard() {
        WizardStatsData newWizardStats = new WizardStatsData();
        newWizardStats.AttackStatsData = new AttackStatsData();
        newWizardStats.DefenceStatsData = new DefenceStatsData();
        newWizardStats.ManaStatsData = new ManaStatsData();
        SaveWizardStatsData(newWizardStats);
        return newWizardStats;
    }
}