using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPanel : MonoBehaviour
{
    [SerializeField] PanelButton[] buttons = new PanelButton[3];

    public void PageChange(int page) {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].setPage(page);
        }
    }

    public void FinishedPageChange() {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].EnableButtons();
        }
    }
}
