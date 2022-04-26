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
        SoundManager.Instance.PlayButtonSound();
        SceneManager.LoadScene("OnlineHost");
    }
    public void OpenOnlinePrivate()
    {
        SoundManager.Instance.PlayButtonSound();
        SceneManager.LoadScene("OnlinePrivate");
    }
    public void OpenOnlineLobby()
    {
        SoundManager.Instance.PlayButtonSound();
        SceneManager.LoadScene("OnlineLobby");
    }
    public void ReturnToDashboard()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
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
