using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class OnlinePrivate : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField iField;
    [SerializeField] ErrorMessage _errorMessage;

    public void OnJoin()
    {
        _errorMessage.DeleteMessage();
        PhotonNetwork.JoinRoom(iField.text);
    }
    public void OnCancel()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        SceneManager.LoadScene("OnlineMenu");
    }
    public override void OnJoinedRoom()
    {
        SoundManager.Instance.StopGameThemeSound();
        SoundManager.Instance.PlayBattleButtonSound();
        PhotonNetwork.AutomaticallySyncScene = true;
        SceneManager.LoadScene("OnlineSearch");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _errorMessage.SetMessage(message);
    }
}
