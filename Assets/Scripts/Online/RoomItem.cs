using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text _roomName;
    [SerializeField] TMP_Text _maxPlayers;
    
    public void UpdateRoomItem(string roomName, int maxPlayers)
    {
        _roomName.text = roomName;
        _maxPlayers.text = maxPlayers + " Players";
    }
}
