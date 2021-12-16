using UnityEngine;
public class Test : MonoBehaviour
{
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    void Start()
    {
        WizardStatsData wizardStatsData1 = new WizardStatsData();
        wizardStatsData1.AttackStatsData = new AttackStatsData();
        wizardStatsData1.AttackStatsData.ArmorPenetration = 2;
        _wizardStatsController.SaveWizardStatsData(wizardStatsData1);
        Debug.Log("yey");
        WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
        Debug.Log(wizardStatsData);
    }
}