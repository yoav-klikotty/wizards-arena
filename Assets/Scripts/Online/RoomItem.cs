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

    public void UpdateRoomItem(string roomName, int maxPlayers, int timeToPlay)
    {
        _roomName.text = roomName;
        _maxPlayers.text = maxPlayers + " Players";
        _timeToPlay.text = timeToPlay + " sec";
    }
}
