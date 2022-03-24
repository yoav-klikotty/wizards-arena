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
    bool isPublic = true;
    [SerializeField] TMP_Text _mode;
    [SerializeField] TMP_Text _numOfPlayers;
    public void ChangeNumberOfPlayers(int players)
    {
        this.numOfPlayers = players;
        _numOfPlayers.text = players + "";
        GameManager.Instance.NumOfDeathmatchPlayers = players;
    }
    public void CreateRoom()
    {
        System.Random random = new System.Random(); 
        RoomOptions roomOps = new RoomOptions() { IsVisible = isPublic, IsOpen = true, MaxPlayers = (byte)numOfPlayers };
        PhotonNetwork.CreateRoom(random.Next(10000,50000) + "", roomOps);
    }

    public override void OnCreatedRoom()
    {
        SceneManager.LoadScene("OnlineSearch");
    }
    public void CancelRoomCreation()
    {
        SceneManager.LoadScene("OnlineMenu");
    }
    public void ChangeMode()
    {
        isPublic = !isPublic;
        _mode.text = isPublic ? "Public" : "Private";
        Debug.Log(isPublic);
    }
}
