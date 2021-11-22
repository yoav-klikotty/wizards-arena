using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] PageSwiper pageSwiper;
    [SerializeField] int panelNum;
    [SerializeField] Image image;
    [SerializeField] GameObject activeFiller;
    [SerializeField] Sprite buttonImage;
    private bool isActive;
    private bool disabled = false;

    void Start(){
        isActive = pageSwiper.getPage() == panelNum;
        if(isActive) {
            activeFiller.SetActive(true);
            Scale(true);
        }
    }

    public void setPage(int page) {
        if(!disabled) {
            disabled = true;
            if (page == panelNum) {
                pageSwiper.setPage(panelNum);
                isActive = true;
                activeFiller.SetActive(true);
                Scale(true);
            }
            else {
                isActive = false;
                activeFiller.SetActive(false);
                Scale(false);
            }
        }
    }

    private void Scale(bool active) {
        if(active) {
            gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        }
        else {
            gameObject.transform.localScale = new Vector3(1f, 1f, 0);
        }
    }

    public void EnableButtons() {
        disabled = false;
    }
}
