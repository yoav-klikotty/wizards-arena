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
                return new WizardStatsData();
            }
        }
        return LocalStorage.LoadWizardStatsData();
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
        EventManager.Instance.UpdateWizardStats();

    }
    void OnEnable()
    {
        EventManager.Instance.levelUpgrade += UpgardeLevel;
    }
    void OnDisable()
    {
        EventManager.Instance.levelUpgrade -= UpgardeLevel;
    }

    public void UpgardeLevel()
    {
        Debug.Log("upgrade level");
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        _wizardStatsData.BaseAttackStatsData.BaseDamage += 3;
        _wizardStatsData.BaseDefenceStatsData.HP += 3;
        _wizardStatsData.BaseManaStatsData.MaxMana += 2;
        SaveWizardStatsData(_wizardStatsData);
    }
}