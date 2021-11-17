using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] PageSwiper pageSwiper;
    [SerializeField] int panelNum;
    [SerializeField] Image image;
    [SerializeField] Sprite buttonImage;
    [SerializeField] Sprite activeButtonImage;
    private bool isActive;

    void Start(){
        isActive = pageSwiper.getPage() == panelNum;
        if(isActive) {
            image.sprite = activeButtonImage;
        }
    }

    public void setPage(int page) {
        if (page == panelNum) {
            pageSwiper.setPage(panelNum);
            isActive = true;
            image.sprite = activeButtonImage;
        }
        else {
            isActive = false;
            image.sprite = buttonImage;
        }
    }
}
