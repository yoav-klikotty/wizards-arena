using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{

    [SerializeField] FilterButton[] _buttons = new FilterButton[4];
    [SerializeField] ItemType _filter;
    [SerializeField] InventoryManager _invetoryManager;

    void Start(){
        FilterClicked((int)_filter);
    }

    public void FilterClicked(int filter){
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetFilter((ItemType)filter);
        }
        _invetoryManager.FilterItems((ItemType)filter);
    }
}
