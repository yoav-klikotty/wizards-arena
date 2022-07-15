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
    void OnEnable()
    {
        EventManager.Instance.updatePlayerStats += UpdatePlayerStats;
    }
    void OnDisable()
    {
        EventManager.Instance.updatePlayerStats -= UpdatePlayerStats;
    }
    void Start()
    {   
        UpdatePlayerStats();
    }

    void UpdatePlayerStats()
    {
        PlayerStatsData playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        _coinsInput.text = playerStatsData.GetCoins() + "";
        _XPInput.text = playerStatsData.GetXP() + "/" + playerStatsData.GetMaxXP();
        _levelBar.value = playerStatsData.GetXP();
        _levelBar.maxValue = playerStatsData.GetMaxXP();
        _energyInput.text = playerStatsData.GetCrystals() + "/" + playerStatsData.GetMaxCrystals();
        _levelInput.text = "" + playerStatsData.GetLevel();
    }
}
