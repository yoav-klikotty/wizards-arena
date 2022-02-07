using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SearchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Slider _progressBar;
    [SerializeField] TMP_Text _progressText;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    // Multiplayer methods
    public override void OnConnectedToMaster()
    {
        IncreaseProgressBar(3);
        _progressText.text = "Connected to server";
        // Invoke("StartBotGame", 15);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom(null, (byte)GameManager.Instance.NumOfDeathmatchPlayers);
    }
    void StartBotGame()
    {
        PhotonNetwork.Disconnect();
    }
    void IncreaseProgressBar(int value)
    {
        _progressBar.value = value;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        IncreaseProgressBar(6);
        _progressText.text = "Find players";
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)GameManager.Instance.NumOfDeathmatchPlayers };
        PhotonNetwork.CreateRoom(null, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == GameManager.Instance.NumOfDeathmatchPlayers && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(GameManager.Instance.NumOfDeathmatchPlayers);
            PhotonNetwork.CurrentRoom.IsOpen = false;
            _progressText.text = "Starting game";
            IncreaseProgressBar(9);
            PhotonNetwork.LoadLevel("Game");
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        _progressText.text = "Starting game";
        IncreaseProgressBar(9);
        SceneManager.LoadScene("Game");
    }
}
