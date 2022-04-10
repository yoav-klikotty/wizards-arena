using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text _roomName;
    [SerializeField] TMP_Text _maxPlayers;
    [SerializeField] TMP_Text _timeToPlay;
    [SerializeField] TMP_Text _cost;

    public void UpdateRoomItem(string roomName, int maxPlayers, int timeToPlay, int cost)
    {
        _roomName.text = roomName;
        _maxPlayers.text = maxPlayers + " Players";
        _timeToPlay.text = timeToPlay + " sec";
        _cost.text = cost + " X";
    }
}
