using System;
public class PlayerStatsController
{
    private PlayerStatsData _playerStatsData;
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

    private PlayerStatsData CreateNewPlayer()
    {
        _playerStatsData = new PlayerStatsData();
        SavePlayerStatsData(_playerStatsData);
        return _playerStatsData;
    }
    
}
