using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text _levelInput;
    [SerializeField] TMP_Text _coinsInput;
    [SerializeField] TMP_Text _energyInput;
    [SerializeField] TMP_Text _XPInput;
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
        _coinsInput.text = playerStatsData.GetCoins() + "";
        _XPInput.text = playerStatsData.GetXP() + "/" + playerStatsData.GetMaxXP();
        _levelBar.value = playerStatsData.GetXP();
        _levelBar.maxValue = playerStatsData.GetMaxXP();
        _energyInput.text = playerStatsData.GetCrystals() + "/" + playerStatsData.GetMaxCrystals();
        _levelInput.text = "" + playerStatsData.GetLevel();
    }
}
