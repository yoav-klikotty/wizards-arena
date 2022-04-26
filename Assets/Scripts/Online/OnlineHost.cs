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
    bool isPublicMode = true;
    bool isCompetitiveMode = true;
    int energyCost = 1;
    [SerializeField] TMP_Text _publicMode;
    [SerializeField] TMP_Text _competitiveMode;
    [SerializeField] TMP_Text _eneryCost;
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
        SoundManager.Instance.PlayButtonSound();
        this.numOfPlayers = players;
        _numOfPlayers.text = players + "";
        GameManager.Instance.NumOfDeathmatchPlayers = players;
    }
    public void ChangeTimeToPlay(int seconds)
    {
        SoundManager.Instance.PlayButtonSound();
        this.secondsToPlay = seconds;
        _secondsToPlay.text = seconds + " sec";
        GameManager.Instance.TimeToPlay = seconds;
    }
    public void CreateRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        _errorMessage.DeleteMessage();
        System.Random random = new System.Random();
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = isPublicMode,
            IsOpen = true,
            MaxPlayers = (byte)numOfPlayers,
            PublishUserId = true,
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "s", GameManager.Instance.TimeToPlay }, { "ec", GameManager.Instance.EnergyCost } },
            CustomRoomPropertiesForLobby = new string[2] { "s", "ec" }
        };
        PhotonNetwork.CreateRoom(random.Next(10000, 50000) + "", roomOps);
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
        SoundManager.Instance.PlayNegativeButtonSound();
        SceneManager.LoadScene("OnlineMenu");
    }
    public void ChangePublicMode()
    {
        SoundManager.Instance.PlayButtonSound();
        isPublicMode = !isPublicMode;
        _publicMode.text = isPublicMode ? "Public" : "Private";
    }
    public void ChangeCompetitiveMode()
    {
        SoundManager.Instance.PlayButtonSound();
        isCompetitiveMode = !isCompetitiveMode;
        if (isCompetitiveMode)
        {
            GameManager.Instance.EnergyCost = 1;
        }
        else {
            GameManager.Instance.EnergyCost = 0;
        }
        _eneryCost.text = "X " + GameManager.Instance.EnergyCost;
        _competitiveMode.text = isCompetitiveMode ? "Competitive" : "Friendly";
    }
}
