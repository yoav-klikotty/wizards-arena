using UnityEngine;
using System.Collections.Generic;

public class WizardStatsController : MonoBehaviour
{
    private WizardStatsData _wizardStatsData;
    public static WizardStatsController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
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

    public void ResetData()
    {
        _wizardStatsData = null;
        PlayerStatsController.Instance.ResetData();
        PlayerPrefs.DeleteAll();
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
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        _wizardStatsData.BaseAttackStatsData.BaseDamage += 3;
        _wizardStatsData.BaseDefenceStatsData.HP += 3;
        _wizardStatsData.BaseManaStatsData.MaxMana += 2;
        SaveWizardStatsData(_wizardStatsData);
    }
    public void SaveDashboardWizard(DashboardWizard dw)
    {
        SaveWizardStatsData(dw.WizardStatsData);
    }
}