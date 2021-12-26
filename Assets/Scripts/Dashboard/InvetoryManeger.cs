using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManeger : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    private GameObject[] _slots;
    private InventoryItem[] _inventoryItems;
    private InventoryDataController _inventoryDataController = new InventoryDataController();
    private List<InventoryItemData> _inventoryDataList;

    void Start()
    {
        // // chunk to write items to local storage
        // string[] list = new string[] {"Advanced_Cape", "Orb", "Staff", "Cape", "Advanced_Orb", "Advanced_Staff"};
        // bool[] equiped = new bool[] {true, true, true, false, false, false};
        // var a = new InventoryData();
        // var b = new List<InventoryItemData>();
        // for (int i = 0; i < list.Length; i++) {
        //     var item = new InventoryItemData();
        //     item.Name = list[i];
        //     item.Equiped = equiped[i];
        //     b.Add(item);
        // }
        // a.Items = b;
        // _inventoryDataController.SaveInventoryData(a);
        // // end chunk

        _inventoryDataList = _inventoryDataController.GetInventoryData().Items;
        _inventoryItems = new InventoryItem[_inventoryDataList.Count];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < _inventoryDataList.Count; i++){
            var prefab = Resources.Load("Prefabs/Items/" + _inventoryDataList[i].Name + "/" + _inventoryDataList[i].Name + "_Inventory");
            var item = Instantiate(prefab, _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform) as GameObject;
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
            _inventoryItems[i].SetEquipedStatus(_inventoryDataList[i].Equiped);
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

    public void EquipItem(InventoryItem itemSelected) {
        string type = itemSelected.GetType();
        string itemSelectedName = itemSelected.GetName();
        string itemRemovedName = "";
        for(int i = 0; i < _inventoryItems.Length; i++){
            if(_inventoryItems[i].GetType() == type && _inventoryItems[i].GetEquipedStatus() == true){
                _inventoryItems[i].SetEquipedStatus(false);
                itemRemovedName = _inventoryItems[i].GetName();
                break;
            }
        }
        itemSelected.SetEquipedStatus(true);
        for (int i = 0; i < _inventoryDataList.Count; i++) {
            if (itemSelectedName == _inventoryDataList[i].Name) {
                _inventoryDataList[i].Equiped = true;
            }
            if (_inventoryDataList[i].Name == itemRemovedName) {
                _inventoryDataList[i].Equiped = false;
            }
        }
        InventoryData inventoryData = new InventoryData();
        inventoryData.Items = _inventoryDataList;
        _inventoryDataController.SaveInventoryData(inventoryData);
    }
}
