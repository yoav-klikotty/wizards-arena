using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPanel : MonoBehaviour
{
    [SerializeField] PanelButton[] _buttons = new PanelButton[3];

    public void PageChange(int page) {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetPage(page);
        }
    }

    public void FinishedPageChange() {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].EnableButtons();
        }
    }
}
