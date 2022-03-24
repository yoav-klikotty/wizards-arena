using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class OnlinePrivate : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField iField;

    public void OnJoin()
    {
        PhotonNetwork.JoinRoom(iField.text);
    }
    public void OnCancel()
    {
        SceneManager.LoadScene("OnlineMenu");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        SceneManager.LoadScene("OnlineSearch");
    }
}
