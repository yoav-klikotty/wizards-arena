using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{

    [SerializeField] FilterButton[] buttons = new FilterButton[5];
    [SerializeField] string filter;

    void Start(){
        FilterClicked(filter);
    }

    public void FilterClicked(string filter){
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetFilter(filter);
        }
    }
}
