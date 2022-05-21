using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MasteryTree : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    public PlayerStatsData playerStatsData;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    public WizardStatsData wizardStatsData;
    [SerializeField] List<InventoryMastery> _masteries = new List<InventoryMastery>();
    [SerializeField] TMP_Text masteriesPoints;

    void Start()
    {
        RefreshTree();
    }
    public void RefreshTree()
    {
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        wizardStatsData = _wizardStatsController.GetWizardStatsData();
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
            _wizardStatsController.SaveWizardStatsData(wizardStatsData);
            playerStatsData.ReduceMasteriesPoints();
            _playerStatsController.SavePlayerStatsData(playerStatsData);
            RefreshTree();
        }

    }

    public void GoToDashboard()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        SceneManager.LoadScene("Dashboard");
    }
}
