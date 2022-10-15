using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSelection : MonoBehaviour
{
    private WizardStatsData _wizardsStatsData;
    [SerializeField] DashboardWizard[] wizards;
    private int currentWizardIdx;
    void Start()
    {
        PlayerPrefs.DeleteAll();
        _wizardsStatsData = WizardStatsController.Instance.GetWizardStatsData();
        for (var i = 0; i < wizards.Length; i++){
            if (wizards[i].WizardStatsData.CapeStatsData.Name.Equals(_wizardsStatsData.CapeStatsData.Name)){
                wizards[i].gameObject.SetActive(true);
                currentWizardIdx = i;
            }
        }
        
    }

    public void OnNextButton()
    {
        wizards[currentWizardIdx].gameObject.SetActive(false);
        currentWizardIdx = mod(currentWizardIdx + 1,wizards.Length);
        wizards[currentWizardIdx].gameObject.SetActive(true);
        WizardStatsController.Instance.SaveDashboardWizard(wizards[currentWizardIdx]);
    }
    public void OnPrevButton()
    {
        wizards[currentWizardIdx].gameObject.SetActive(false);
        currentWizardIdx = mod(currentWizardIdx - 1,wizards.Length);
        wizards[currentWizardIdx].gameObject.SetActive(true);
        WizardStatsController.Instance.SaveDashboardWizard(wizards[currentWizardIdx]);
    }
    int mod(int x, int m) {
        return (x%m + m)%m;
    }
}
