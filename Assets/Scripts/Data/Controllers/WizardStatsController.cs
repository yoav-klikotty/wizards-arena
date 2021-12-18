using UnityEngine;

public class WizardStatsController
{
    private WizardStatsData _wizardStatsData; 
    public WizardStatsData GetWizardStatsData()
    {
        if (_wizardStatsData == null)
        {
            _wizardStatsData = LocalStorage.LoadWizardStatsData();
        }
        return _wizardStatsData;
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
    }
}