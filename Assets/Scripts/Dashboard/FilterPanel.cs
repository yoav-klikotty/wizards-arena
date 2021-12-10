using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{

    [SerializeField] FilterButton[] _buttons = new FilterButton[5];
    [SerializeField] string _filter;
    [SerializeField] GameObject _unfilteredContaier;
    private GameObject[] _slots;
    private InventoryItem[] _items;

    void Start(){
        FilterClicked(_filter);
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        _items = GameObject.FindObjectsOfType<InventoryItem>();
        Debug.Log(_items);
    }

    public void FilterClicked(string filter){
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetFilter(filter);
        }
        for(int i = 0; i < _items.Length; i++) {
            _items[i].gameObject.transform.SetParent(_unfilteredContaier.transform);
        }
    }
}
