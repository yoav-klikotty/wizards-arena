using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text _nameInput;
    [SerializeField] TMP_Text _levelInput;
    [SerializeField] TMP_Text _coinsInput;
    [SerializeField] TMP_Text _energyInput;
    [SerializeField] Slider _levelBar;

    private PlayerStatsController _playerStatsController = new PlayerStatsController();

    void OnEnable()
    {
        PlayerStatsController.UpdateEvent += UpdatePlayerStats;
    }


    void OnDisable()
    {
        PlayerStatsController.UpdateEvent -= UpdatePlayerStats;
    }


    void Start()
    {   
        UpdatePlayerStats();
    }

    void UpdatePlayerStats()
    {
        PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
        _nameInput.text = playerStatsData.GetName();
        _coinsInput.text = playerStatsData.GetCoins() + "";
        _energyInput.text = playerStatsData.GetCrystals() + "/" + playerStatsData.GetMaxCrystals();
        _levelInput.text = "level: " + playerStatsData.GetLevel();
        _levelBar.value = playerStatsData.GetLevelPoints();
    }
}
