using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class OnlineHost : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    int numOfPlayers = 2;
    int secondsToPlay = 2;
    bool isPublic = true;
    [SerializeField] TMP_Text _mode;
    [SerializeField] TMP_Text _numOfPlayers;
    [SerializeField] TMP_Text _secondsToPlay;
    [SerializeField] ErrorMessage _errorMessage;

    void Start()
    {
        ChangeNumberOfPlayers(GameManager.Instance.NumOfDeathmatchPlayers);
        ChangeTimeToPlay(GameManager.Instance.TimeToPlay);
    }

    public void ChangeNumberOfPlayers(int players)
    {
        this.numOfPlayers = players;
        _numOfPlayers.text = players + "";
        GameManager.Instance.NumOfDeathmatchPlayers = players;
    }
    public void ChangeTimeToPlay(int seconds)
    {
        this.secondsToPlay = seconds;
        _secondsToPlay.text = seconds + " sec";
        GameManager.Instance.TimeToPlay = seconds;
    }
    public void CreateRoom()
    {
        _errorMessage.DeleteMessage();
        System.Random random = new System.Random(); 
        RoomOptions roomOps = new RoomOptions() { IsVisible = isPublic, IsOpen = true, MaxPlayers = (byte)numOfPlayers,
         CustomRoomProperties =  new ExitGames.Client.Photon.Hashtable()  { { "s", GameManager.Instance.TimeToPlay } },
         CustomRoomPropertiesForLobby = new string[1] { "s" }
        };
        PhotonNetwork.CreateRoom(random.Next(10000,50000) + "", roomOps);
    }

    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("OnlineSearch");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorMessage.SetMessage(message);
    }
    public void CancelRoomCreation()
    {
        SceneManager.LoadScene("OnlineMenu");
    }
    public void ChangeMode()
    {
        isPublic = !isPublic;
        _mode.text = isPublic ? "Public" : "Private";
    }
}
