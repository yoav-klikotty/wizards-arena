using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    [SerializeField] Player Player;
    private GameObject[] _slots;
    private InventoryItem[] _inventoryItems;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    void Start()
    {
        WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
        string[] list = new string[] {"Blue_Cape", "Blue_Orb", "Blue_Staff", "Dark_Blue_Cape", "Dark_Blue_Orb", "Dark_Blue_Staff"};
        _inventoryItems = new InventoryItem[list.Length];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < list.Length; i++){
            var prefab = Resources.Load("Prefabs/Items/" + list[i]);
            var item = Instantiate(prefab, _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform) as GameObject;
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
            if (_inventoryItems[i].GetItemType() == "Cape")
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.CapeStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
            if (_inventoryItems[i].GetItemType() == "Staff")
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.StaffStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
            if (_inventoryItems[i].GetItemType() == "Orb")
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.OrbStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
        }
        wizardStatsData.WriteWizardStats();
    }

    public void FilterItems(string type) {
        for(int i = 0; i < _inventoryItems.Length; i++) {
            // do not include equiped items
            _inventoryItems[i].transform.SetParent(_unfilteredContaier.transform, false);
        }
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        for(int i = 0; i < _inventoryItems.Length; i++) {
            if(_inventoryItems[i].GetItemType() == type) {
                filteredItems.Add(_inventoryItems[i]);
            }
        }
        for(int i = 0; i < filteredItems.Count; i++) {
            filteredItems[i].transform.SetParent(_slots[i].transform, false);
        }
    }

    public void EquipItem(InventoryItem itemSelected) {
        WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
        wizardStatsData.EquipItem(itemSelected);
        string type = itemSelected.GetItemType();
        string itemSelectedName = itemSelected.GetName();
        string itemRemovedName = "";
        for(int i = 0; i < _inventoryItems.Length; i++){
            if(_inventoryItems[i].GetItemType() == type && _inventoryItems[i].GetEquipedStatus() == true){
                _inventoryItems[i].SetEquipedStatus(false);
                wizardStatsData.RemoveItem(_inventoryItems[i]);
                itemRemovedName = _inventoryItems[i].GetName();
                break;
            }
        }
        itemSelected.SetEquipedStatus(true);
        _wizardStatsController.SaveWizardStatsData(wizardStatsData);
        wizardStatsData.WriteWizardStats();
        Player.UpdateWizard();
    }
}
