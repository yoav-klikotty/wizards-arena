using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class OnlineMenu : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void OpenOnlineHost()
    {
        SceneManager.LoadScene("OnlineHost");

    }
    public void OpenOnlinePrivate()
    {
        SceneManager.LoadScene("OnlinePrivate");

    }
    public void OpenOnlineLobby()
    {
        SceneManager.LoadScene("OnlineLobby");

    }
    public void ReturnToDashboard()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected (DisconnectCause cause)
    {
        Debug.Log(cause);
        SceneManager.LoadScene("Dashboard");
    }
}
