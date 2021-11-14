using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] PageSwiper pageSwiper;
    [SerializeField] int panelNum;
    private bool isActive;

    void Start(){
        isActive = pageSwiper.getPage() == panelNum;
    }

    public void setPage() {
        pageSwiper.setPage(panelNum);
        isActive = true;
    }
}
