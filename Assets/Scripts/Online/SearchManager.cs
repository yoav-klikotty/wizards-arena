using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;

public class SearchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _roomCode;
    [SerializeField] TMP_Text _progressText;

    void Start()
    {
        UpdatePlayersCount();
        GameManager.Instance.TimeToPlay = (int)PhotonNetwork.CurrentRoom.CustomProperties["s"];
        if (PhotonNetwork.IsMasterClient)
        {
            _roomCode.text = "Room code: " + PhotonNetwork.CurrentRoom.Name;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        UpdatePlayersCount();
        if (PhotonNetwork.CurrentRoom.PlayerCount == GameManager.Instance.NumOfDeathmatchPlayers && PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(StartGame());
        }
    }
    private void UpdatePlayersCount()
    {
        var playersLeftToFind = PhotonNetwork.CurrentRoom.MaxPlayers - PhotonNetwork.CurrentRoom.PlayerCount;
        _progressText.text = "waiting for " + playersLeftToFind + " more players";
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("Game");
    }
}
