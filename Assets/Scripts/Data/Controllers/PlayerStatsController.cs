public class PlayerStatsController
{
    public delegate void DataChangedAction();
    public static event DataChangedAction UpdateEvent;

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
        if (UpdateEvent != null)
        {
            UpdateEvent();
        }
    }

    private PlayerStatsData CreateNewPlayer()
    {
        _playerStatsData = new PlayerStatsData();
        SavePlayerStatsData(_playerStatsData);
        return _playerStatsData;
    }
}
