using UnityEngine;
using System.Collections.Generic;

public class WizardStatsController
{
    public delegate void DataChangedAction();
    public static event DataChangedAction UpdateEvent;
    private WizardStatsData _wizardStatsData;
    public WizardStatsData GetWizardStatsData()
    {
        if (_wizardStatsData == null)
        {
            _wizardStatsData = LocalStorage.LoadWizardStatsData();
            if (_wizardStatsData == null)
            {
                return new WizardStatsData();
            }
        }
        return LocalStorage.LoadWizardStatsData();
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
        if (UpdateEvent != null)
        {
            UpdateEvent();
        }
    }
}