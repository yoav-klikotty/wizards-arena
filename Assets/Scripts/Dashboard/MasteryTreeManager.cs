using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasteryTreeManager : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    PlayerStatsData playerStatsData;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    WizardStatsData wizardStatsData;
    private int _currentTier;
    [SerializeField] List<InventoryMasteries> _nestedMasteryTree = new List<InventoryMasteries>();

    void Start()
    {
        RefreshTree();
    }
    public void RefreshTree()
    {
        _currentTier = 1;
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        wizardStatsData = _wizardStatsController.GetWizardStatsData();
        CalculateTreeTier();
        RenderNestedSkillTree();
    }
    public void CalculateTreeTier()
    {
        foreach (var inventoryMasteries in _nestedMasteryTree)
        {
            bool isTierFull = true;
            foreach (var inventoryMastery in inventoryMasteries.masteries)
            {
                var mastery = wizardStatsData.FindMastery(inventoryMastery.GetID());
                if (mastery == null || mastery.points < mastery.maxPoints)
                {
                    isTierFull = false;
                }
                if (mastery != null && mastery.points > 0)
                {
                    inventoryMastery.SetCurrentPoints(mastery.points);
                }
            }
            if (isTierFull)
            {
                IncreaseCurrentTier();
            }
        }

    }
    public void IncreaseCurrentTier()
    {
        if (_currentTier < _nestedMasteryTree.Count)
        {
            _currentTier += 1;
        }
    }
    public void RenderNestedSkillTree()
    {
        for (int i = 0; i < _currentTier; i++)
        {
            var inventoryMasteries = _nestedMasteryTree[i];
            foreach (var mastery in inventoryMasteries.masteries)
            {
                if (playerStatsData.GetLevel() >= mastery.GetRequiredLevel())
                {
                    mastery.EnableMastery();
                }
            }
        }
        for (int i = _currentTier; i < _nestedMasteryTree.Count; i++)
        {
            var inventoryMasteries = _nestedMasteryTree[i];
            foreach (var mastery in inventoryMasteries.masteries)
            {
                mastery.DisableMastery();
            }
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
