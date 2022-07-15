using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPanel : MonoBehaviour
{
    [SerializeField] PanelButton[] _buttons = new PanelButton[5];

    public void PageChange(int page) {
        SoundManager.Instance.PlayButtonSound();
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetPage(page);
        }
    }
}
