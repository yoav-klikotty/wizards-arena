using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    [SerializeField] Player Player;
    private GameObject[] _slots;
    private InventoryItem[] _inventoryItems;
    WizardStatsController _wizardStatsController = new WizardStatsController();
    private PlayerStatsController _playerStatsController = new PlayerStatsController();

    void Start()
    {
        PlayerPrefs.DeleteAll();
        WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
        PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
        InventoryPrefabs[] totalItems = new InventoryPrefabs[] {
            new InventoryPrefabs("Blue_Cape", ItemType.Cape),
            new InventoryPrefabs("Blue_Orb", ItemType.Orb),
            new InventoryPrefabs("Blue_Staff", ItemType.Staff),
            new InventoryPrefabs("Dark_Blue_Cape", ItemType.Cape),
            new InventoryPrefabs("Dark_Blue_Orb", ItemType.Orb),
            new InventoryPrefabs("Dark_Blue_Staff", ItemType.Staff),
            new InventoryPrefabs("Green_Cape", ItemType.Cape),
            new InventoryPrefabs("Green_Orb", ItemType.Orb),
            new InventoryPrefabs("Green_Staff", ItemType.Staff),
            new InventoryPrefabs("Lava_Cape", ItemType.Cape),
            new InventoryPrefabs("Lava_Orb", ItemType.Orb),
            new InventoryPrefabs("Lava_Staff", ItemType.Staff),
            new InventoryPrefabs("Red_Cape", ItemType.Cape),
            new InventoryPrefabs("Red_Orb", ItemType.Orb),
            new InventoryPrefabs("Red_Staff", ItemType.Staff),
        };
        _inventoryItems = new InventoryItem[totalItems.Length];
        _slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for (int i = 0; i < totalItems.Length; i++)
        {
            var prefab = Resources.Load("Prefabs/Items/" + totalItems[i].Type.ToString() + "/" + totalItems[i].Name);
            var item = Instantiate(prefab, _slots[i].transform.position, _slots[i].transform.rotation, _slots[i].transform) as GameObject;
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
            if (_inventoryItems[i].GetItemType() == ItemType.Cape)
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.CapeStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
            if (_inventoryItems[i].GetItemType() == ItemType.Staff)
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.StaffStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
            if (_inventoryItems[i].GetItemType() == ItemType.Orb)
            {
                _inventoryItems[i].SetEquipedStatus(wizardStatsData.OrbStatsData.IsContainInventoryItem(_inventoryItems[i]));
            }
            _inventoryItems[i].SetPurchasedStatus(playerStatsData.IsPurchasedItem(_inventoryItems[i].GetName()));
        }
        wizardStatsData.WriteWizardStats();
    }

    public void FilterItems(ItemType type)
    {
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            _inventoryItems[i].transform.SetParent(_unfilteredContaier.transform, false);
        }
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i].GetItemType() == type)
            {
                filteredItems.Add(_inventoryItems[i]);
            }
        }
        for (int i = 0; i < filteredItems.Count; i++)
        {
            filteredItems[i].transform.SetParent(_slots[i].transform, false);
        }
    }

    public void EquipItem(InventoryItem itemSelected)
    {
        WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
        wizardStatsData.EquipItem(itemSelected);
        ItemType type = itemSelected.GetItemType();
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i].GetItemType() == type && _inventoryItems[i].GetEquipedStatus() == true)
            {
                _inventoryItems[i].SetEquipedStatus(false);
                wizardStatsData.RemoveItem(_inventoryItems[i]);
                break;
            }
        }
        itemSelected.SetEquipedStatus(true);
        _wizardStatsController.SaveWizardStatsData(wizardStatsData);
        wizardStatsData.WriteWizardStats();
        Player.UpdateWizard(null);
    }

    public void PurchaseItem(InventoryItem itemSelected)
    {
        PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
        playerStatsData.AddItem(itemSelected);
        _playerStatsController.SavePlayerStatsData(playerStatsData, true);
    }
}
