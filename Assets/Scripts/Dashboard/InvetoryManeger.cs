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

        var itemsData = _inventoryDataController.GetInventoryData();
        var itemsList = itemsData.Items;
        _inventoryItems = new InventoryItem[itemsList.Count+3];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        InstantiateSinglePrefab(itemsData.EquipedCape, 0);
        InstantiateSinglePrefab(itemsData.EquipedStaff, 1);
        InstantiateSinglePrefab(itemsData.EquipedOrb, 2);
        for(int i = 0; i < itemsList.Count; i++){
            var prefab = Resources.Load("Prefabs/Items/" + itemsList[i].Name + "/" + itemsList[i].Name + "_Inventory");
            var item = Instantiate(prefab, _slots[i+3].transform.position, _slots[i+3].transform.rotation, _slots[i+3].transform) as GameObject;
            _inventoryItems[i+3] = item.GetComponent<InventoryItem>();
            _inventoryItems[i+3].SetData(itemsList[i]);
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

    private void InstantiateSinglePrefab(InventoryItemData itemData, int position) {
        var prefab = Resources.Load("Prefabs/Items/" + itemData.Name + "/" + itemData.Name + "_Inventory");
        var item = Instantiate(prefab, _slots[position].transform.position, _slots[position].transform.rotation, _slots[position].transform) as GameObject;
        _inventoryItems[position] = item.GetComponent<InventoryItem>();
        _inventoryItems[position].SetData(itemData);
    }
}
