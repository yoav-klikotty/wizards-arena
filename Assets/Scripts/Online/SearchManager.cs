using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using System;

public class SearchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _roomCode;
    [SerializeField] TMP_Text _progressText;
    int timeLimit = 10;
    DateTime start;
    bool flag;
    void Start()
    {
        UpdatePlayersCount();
        GameManager.Instance.TimeToPlay = (int)PhotonNetwork.CurrentRoom.CustomProperties["s"];
        GameManager.Instance.NumOfDeathmatchPlayers = (int)PhotonNetwork.CurrentRoom.MaxPlayers;
        if (PhotonNetwork.IsMasterClient)
        {
            start = DateTime.Now;
            _roomCode.text = "Room code: " + PhotonNetwork.CurrentRoom.Name;
        }
    }

    void Update()
    {
        if (Tweaks.BotModeActive)
        {
            if (PhotonNetwork.IsMasterClient && ((DateTime.Now - start).TotalSeconds > timeLimit) && !flag)
            {
                flag = true;
                StartCoroutine(StartGame());
            }
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        UpdatePlayersCount();
        if (PhotonNetwork.CurrentRoom.PlayerCount == GameManager.Instance.NumOfDeathmatchPlayers && PhotonNetwork.IsMasterClient && !flag)
        {
            flag = true;
            StartCoroutine(StartGame());
        }
    }
    private void UpdatePlayersCount()
    {
        GameManager.Instance.ActivePlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        var playersLeftToFind = PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.CurrentRoom.PlayerCount;
        _progressText.text = "waiting for " + playersLeftToFind + " more players";
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("Game");
    }
}
