using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnlineLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _roomItem;
    [SerializeField] GameObject _lobbyContainer;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    [SerializeField] ErrorMessage _errorMessage;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
        UpdateRoomItems();
    }
    private void UpdateRoomItems()
    {
        foreach (Transform child in _lobbyContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var roomInfo in cachedRoomList)
        {
            RoomInfo info = roomInfo.Value;
            var roomItemPref = Instantiate(_roomItem, Vector3.zero, Quaternion.identity);
            roomItemPref.transform.SetParent(_lobbyContainer.transform);
            var roomItem = roomItemPref.GetComponent<RoomItem>();
            roomItem.UpdateRoomItem(info.Name, info.MaxPlayers, (int)info.CustomProperties["s"], (int)info.CustomProperties["ec"]);
            roomItemPref.GetComponentInChildren<Button>().onClick.AddListener(() => { 
                _errorMessage.DeleteMessage();
                PhotonNetwork.JoinRoom(info.Name);
            });
        }
    }
    public override void OnJoinedRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        cachedRoomList.Clear();
        PhotonNetwork.AutomaticallySyncScene = true;
        SceneManager.LoadScene("OnlineSearch");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _errorMessage.SetMessage(message);
    }

    public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }

    public void OnBack()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        SceneManager.LoadScene("OnlineMenu");
    }

}
