using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
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

    public void SavePlayerStatsData(PlayerStatsData playerStatsData, bool isUpdate)
    {
        LocalStorage.SavePlayerStatsData(playerStatsData);
        if (isUpdate)
        {
            UpdateEvent();
        }
    }

    private PlayerStatsData CreateNewPlayer()
    {
        _playerStatsData = new PlayerStatsData();
        _playerStatsData._items.Add("Blue_Cape");
        _playerStatsData._items.Add("Blue_Orb");
        _playerStatsData._items.Add("Blue_Staff");
        SavePlayerStatsData(_playerStatsData, true);
        return _playerStatsData;
    }
}
