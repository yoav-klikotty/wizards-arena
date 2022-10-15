using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ArenaManager : MonoBehaviourPunCallbacks
{
    PlayerStatsData playerStatsData;
    [SerializeField] GameObject _modesButton;
    [SerializeField] GameObject _battlePanel;
    [SerializeField] GameObject _playVsFriendPanel;
    [SerializeField] GameObject _wizardSelection;
    [SerializeField] TMP_InputField iField;

    void Start()
    {
        playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        SoundManager.Instance.PlayGameThemeSound();
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void JoinRoom(int numberOfPlayers)
    {
        if (PhotonNetwork.IsConnected)
        {
            GameManager.Instance.IsPrivateGame = false;
            GameManager.Instance.NumOfPlayers = numberOfPlayers;
            PhotonNetwork.JoinRandomRoom(
                new ExitGames.Client.Photon.Hashtable() { { "s", GameManager.Instance.TimeToPlay }, { "ec", GameManager.Instance.EnergyCost } }, (byte)GameManager.Instance.NumOfPlayers);
        }
    }
    public void JoinPrivateRoom()
    {
        GameManager.Instance.IsPrivateGame = true;
        PhotonNetwork.JoinRoom(iField.text);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        System.Random random = new System.Random();
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)GameManager.Instance.NumOfPlayers,
            PublishUserId = true,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "s", GameManager.Instance.TimeToPlay }, { "ec", GameManager.Instance.EnergyCost } },
            CustomRoomPropertiesForLobby = new string[2] { "s", "ec" }
        };
        PhotonNetwork.CreateRoom(random.Next(10000, 50000) + "", roomOps);
    }
    public void CreateRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        GameManager.Instance.IsPrivateGame = true;
        System.Random random = new System.Random();
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = false,
            IsOpen = true,
            MaxPlayers = (byte)GameManager.Instance.NumOfPlayers,
            PublishUserId = true,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "s", GameManager.Instance.TimeToPlay }, { "ec", GameManager.Instance.EnergyCost } },
            CustomRoomPropertiesForLobby = new string[2] { "s", "ec" }
        };
        PhotonNetwork.CreateRoom(random.Next(10000, 50000) + "", roomOps);
    }
    public override void OnCreatedRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        SceneManager.LoadScene("OnlineSearch");
    }
    public override void OnJoinedRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        SceneManager.LoadScene("OnlineSearch");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
    public void ChangeNumberOfPlayers(int players)
    {
        GameManager.Instance.NumOfPlayers = players;
    }
    public void ChangeTimeToPlay(int seconds)
    {
        GameManager.Instance.TimeToPlay = seconds;
    }

    public void OpenBattlePanel()
    {
        SoundManager.Instance.PlayButtonSound();
        _modesButton.SetActive(false);
        _battlePanel.SetActive(true);
        _playVsFriendPanel.SetActive(false);

    }
    public void OpenPlayVSFriendPanel()
    {
        SoundManager.Instance.PlayButtonSound();
        _modesButton.SetActive(false);
        _battlePanel.SetActive(false);
        _playVsFriendPanel.SetActive(true);
        _wizardSelection.SetActive(false);
    }
    public void ClosePanels()
    {
        _modesButton.SetActive(true);
        _battlePanel.SetActive(false);
        _playVsFriendPanel.SetActive(false);
        _wizardSelection.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

    }

}
