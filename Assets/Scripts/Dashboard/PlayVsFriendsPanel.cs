using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayVsFriendsPanel : MonoBehaviour
{
    [SerializeField] GameObject _buttons;
    [SerializeField] GameObject _createRoomPanel;
    [SerializeField] GameObject _joinRoomPanel;
    [SerializeField] GameObject _actionButton;
    [SerializeField] TMP_Text _actionButtonText;
    [SerializeField] GameObject _backButton;
    [SerializeField] ArenaManager _arenaManager;

    public void ClosePanels()
    {
        _buttons.SetActive(true);
        _createRoomPanel.SetActive(false);
        _joinRoomPanel.SetActive(false);
        _actionButton.SetActive(false);
        _backButton.SetActive(false);

    }
    public void OpenCreateRoomPanel()
    {
        _buttons.SetActive(false);
        _createRoomPanel.SetActive(true);
        _joinRoomPanel.SetActive(false);
        _actionButton.SetActive(true);
        _actionButtonText.text = "Create";
        _actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _actionButton.GetComponent<Button>().onClick.AddListener(() => _arenaManager.CreateRoom());
        _backButton.SetActive(true);

    }
    public void OpenJoinRoomPanel()
    {
        _buttons.SetActive(false);
        _createRoomPanel.SetActive(false);
        _joinRoomPanel.SetActive(true);
        _actionButton.SetActive(true);
        _actionButtonText.text = "Join";
        _actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _actionButton.GetComponent<Button>().onClick.AddListener(() => _arenaManager.JoinPrivateRoom());
        _backButton.SetActive(true);

    }

}
