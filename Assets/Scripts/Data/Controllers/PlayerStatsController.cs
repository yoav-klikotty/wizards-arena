using UnityEngine;
public class PlayerStatsController: MonoBehaviour
{
    private PlayerStatsData _playerStatsData;
    public static PlayerStatsController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public PlayerStatsData GetPlayerStatsData()
    {
        if (_playerStatsData == null)
        {
            _playerStatsData = LocalStorage.LoadPlayerStatsData();
            if (_playerStatsData == null)
            {
                return CreateNewPlayer();
            }
        }
        return LocalStorage.LoadPlayerStatsData();
    }

    public void SavePlayerStatsData(PlayerStatsData playerStatsData)
    {
        LocalStorage.SavePlayerStatsData(playerStatsData);
        EventManager.Instance.UpdatePlayerStats();
    }
    public void ResetData()
    {
        _playerStatsData = null;
    }

    private PlayerStatsData CreateNewPlayer()
    {
        _playerStatsData = new PlayerStatsData();
        SavePlayerStatsData(_playerStatsData);
        return _playerStatsData;
    }
    
}
