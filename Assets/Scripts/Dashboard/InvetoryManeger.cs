using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManeger : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    private GameObject[] _slots;
    private InventoryItem[] _inventoryItems;
    private InventoryDataController _inventoryDataController = new InventoryDataController();


    void Start()
    {
        // // chunk to write items to local storage
        // string[] list = new string[] {"Advanced_Cape", "Orb", "Staff", "Cape", "Advanced_Orb", "Advanced_Staff"};
        // var a = new InventoryData();
        // var b = new List<InventoryItemData>();
        // for (int i = 0; i < list.Length; i++) {
        //     var item = new InventoryItemData();
        //     item.Name = list[i];
        //     b.Add(item);
        // }
        // a.Items = b;
        // _inventoryDataController.SaveInventoryData(a);
        // // end chunk

        var itemsList = _inventoryDataController.GetInventoryData().Items;
        _inventoryItems = new InventoryItem[itemsList.Count];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < itemsList.Count; i++){
            var prefab = Resources.Load("Items/" + itemsList[i].Name + "/" + itemsList[i].Name + "_Inventory");
            var item = Instantiate(prefab, _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform) as GameObject;
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
            _inventoryItems[i].SetData();
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
