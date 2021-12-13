using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManeger : MonoBehaviour
{
    [SerializeField] string[] _items;
    [SerializeField] InventoryItem[] _prefabs;
    [SerializeField] GameObject _unfilteredContaier;
    private GameObject[] _slots;
    private InventoryItem[] _itemsObject;


    void Start()
    {
        _itemsObject = new InventoryItem[_items.Length];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < _items.Length; i++){
            var item = Instantiate(_prefabs[i%_prefabs.Length], _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform);
            _itemsObject[i] = item;
        }
    }

    public void FilterItems(string type) {
        for(int i = 0; i < _itemsObject.Length; i++) {
            _itemsObject[i].transform.SetParent(_unfilteredContaier.transform, false);
        }
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        for(int i = 0; i < _itemsObject.Length; i++) {
            if(type == "all" || _itemsObject[i].GetType() == type) {
                filteredItems.Add(_itemsObject[i]);
            }
        }
        for(int i = 0; i < filteredItems.Count; i++) {
            filteredItems[i].transform.SetParent(_slots[i].transform, false);
        }
    }
}
