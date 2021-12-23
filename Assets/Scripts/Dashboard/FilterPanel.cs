using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{

    [SerializeField] FilterButton[] _buttons = new FilterButton[4];
    [SerializeField] string _filter;
    [SerializeField] InvetoryManeger _invetoryManeger;

    void Start(){
        FilterClicked(_filter);
    }

    public void FilterClicked(string filter){
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetFilter(filter);
        }
        _invetoryManeger.FilterItems(filter);
    }
}
