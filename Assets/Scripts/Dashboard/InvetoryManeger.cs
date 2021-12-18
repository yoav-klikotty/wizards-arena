using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManeger : MonoBehaviour
{
    [SerializeField] InventoryItem[] _prefabs;
    [SerializeField] GameObject _unfilteredContaier;
    private GameObject[] _slots;
    private InventoryItem[] _inventoryItems;
    private InventoryDataController _inventoryDataController = new InventoryDataController();


    void Start()
    {
        var list = _inventoryDataController.GetInventoryData().Items;
        _inventoryItems = new InventoryItem[list.Count];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < list.Count; i++){
            var prefab = Resources.Load("Items/" + list[i] + "/" + list[i] + "_Inventory");
            var item = Instantiate(prefab, _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform) as GameObject;
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
        }
    }

    public void FilterItems(string type) {
        for(int i = 0; i < _inventoryItems.Length; i++) {
            // do not include equiped items
            _inventoryItems[i].transform.SetParent(_unfilteredContaier.transform, false);
        }
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        for(int i = 0; i < _inventoryItems.Length; i++) {
            if(_inventoryItems[i].GetType() == type) {
                filteredItems.Add(_inventoryItems[i]);
            }
        }
        for(int i = 0; i < filteredItems.Count; i++) {
            filteredItems[i].transform.SetParent(_slots[i].transform, false);
        }
    }
}
