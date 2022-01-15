using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();

    [SerializeField] NavigationPanel _navigationPanel;
    public void StartGame()
    {
        PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
        if (playerStatsData.GetCrystals() > 0)
        {
            playerStatsData.SetCrystals(playerStatsData.GetCrystals() - 1);
            _playerStatsController.SavePlayerStatsData(playerStatsData, true);
            SceneManager.LoadScene("Search");
        }
        else 
        {
            _navigationPanel.PageChange(3);
        }
    }
}
