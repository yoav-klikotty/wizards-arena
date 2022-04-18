using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasteryTree : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    public PlayerStatsData playerStatsData;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    public WizardStatsData wizardStatsData;
    [SerializeField] List<InventoryMastery> _masteries = new List<InventoryMastery>();

    void Start()
    {
        RefreshTree();
    }
    public void RefreshTree()
    {
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        wizardStatsData = _wizardStatsController.GetWizardStatsData();
        foreach (var mastery in _masteries)
        {
            mastery.Validate(wizardStatsData, playerStatsData);
        }
    }
    public void UpdateSkill(InventoryMastery inventoryMastery)
    {
        if (playerStatsData.GetXP() > 0)
        {
            inventoryMastery.SetCurrentPoints(inventoryMastery.GetCurrentPoints() + 1);
            wizardStatsData.UpdateMasteryPoints(inventoryMastery);
            _wizardStatsController.SaveWizardStatsData(wizardStatsData);
            playerStatsData.SetXP(playerStatsData.GetXP() - 1);
            _playerStatsController.SavePlayerStatsData(playerStatsData, true);
            RefreshTree();
        }

    }

    public void GoToDashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }
}
