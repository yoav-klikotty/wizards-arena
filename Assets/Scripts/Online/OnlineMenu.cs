using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections.Generic;
public class OnlineMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Button> _menuButtons;
    [SerializeField] ErrorMessage _errorMessage;

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            foreach (var button in _menuButtons)
            {
                button.interactable = true;
            }
        }
    }
    public override void OnConnectedToMaster()
    {
        foreach (var button in _menuButtons)
        {
            button.interactable = true;
        }
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
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
        if (cause == DisconnectCause.DisconnectByClientLogic)
        {
            SceneManager.LoadScene("Dashboard");
        }
        else
        {
            _errorMessage.SetMessage(cause.ToString());
        }
    }
}