using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MasteryTree : MonoBehaviour
{
    public PlayerStatsData playerStatsData;
    public WizardStatsData wizardStatsData;
    [SerializeField] List<InventoryMastery> _masteries = new List<InventoryMastery>();
    [SerializeField] TMP_Text masteriesPoints;

    void Start()
    {
        RefreshTree();
    }
    public void RefreshTree()
    {
        playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        wizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
        masteriesPoints.text = "x " + playerStatsData.GetMasteriesPoints();
        foreach (var mastery in _masteries)
        {
            mastery.Validate(wizardStatsData, playerStatsData);
        }
    }
    public void UpdateSkill(InventoryMastery inventoryMastery)
    {
        if (playerStatsData.GetMasteriesPoints() > 0)
        {
            inventoryMastery.SetCurrentPoints(inventoryMastery.GetCurrentPoints() + 1);
            wizardStatsData.UpdateMasteryPoints(inventoryMastery);
            WizardStatsController.Instance.SaveWizardStatsData(wizardStatsData);
            playerStatsData.ReduceMasteriesPoints();
            PlayerStatsController.Instance.SavePlayerStatsData(playerStatsData);
            RefreshTree();
        }

    }

    public void GoToDashboard()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        SceneManager.LoadScene("Dashboard");
    }
}
