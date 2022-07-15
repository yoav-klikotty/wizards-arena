using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    [SerializeField] GameObject _btnContainer;
    private InventoryItem[] _inventoryItems;
    private InventoryPrefabs[] totalItems = new InventoryPrefabs[] {
            new InventoryPrefabs("Grey_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Grey_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Grey_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Green_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Green_Advanced_Cape", ItemType.Cape),
            new InventoryPrefabs("Blue_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Blue_Advanced_Cape", ItemType.Cape),
            new InventoryPrefabs("Red_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Red_Advanced_Cape", ItemType.Cape),
            new InventoryPrefabs("Purple_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Purple_Advanced_Cape", ItemType.Cape),
            new InventoryPrefabs("Yellow_Basic_Cape", ItemType.Cape),
            new InventoryPrefabs("Yellow_Advanced_Cape", ItemType.Cape),
            new InventoryPrefabs("Green_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Green_Advanced_Orb", ItemType.Orb),
            new InventoryPrefabs("Blue_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Blue_Advanced_Orb", ItemType.Orb),
            new InventoryPrefabs("Red_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Red_Advanced_Orb", ItemType.Orb),
            new InventoryPrefabs("Purple_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Purple_Advanced_Orb", ItemType.Orb),
            new InventoryPrefabs("Yellow_Basic_Orb", ItemType.Orb),
            new InventoryPrefabs("Yellow_Advanced_Orb", ItemType.Orb),
            new InventoryPrefabs("Green_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Green_Advanced_Staff", ItemType.Staff),
            new InventoryPrefabs("Blue_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Blue_Advanced_Staff", ItemType.Staff),
            new InventoryPrefabs("Red_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Red_Advanced_Staff", ItemType.Staff),
            new InventoryPrefabs("Purple_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Purple_Advanced_Staff", ItemType.Staff),
            new InventoryPrefabs("Yellow_Basic_Staff", ItemType.Staff),
            new InventoryPrefabs("Yellow_Advanced_Staff", ItemType.Staff),
    };
    void Start()
    {
        InitializeInventory();
        InitializeWizardSavedItems();
    }

    private void InitializeWizardSavedItems()
    {
        WizardStatsData wizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
        WizardStatsController.Instance.SaveWizardStatsData(wizardStatsData);
    }
    private void InitializeInventory()
    {
        _inventoryItems = new InventoryItem[totalItems.Length];
        for (int i = 0; i < totalItems.Length; i++)
        {
            var prefab = Resources.Load("Prefabs/Items/" + totalItems[i].Type.ToString() + "/" + totalItems[i].Name);
            var item = Instantiate(prefab,  Vector3.zero, Quaternion.identity) as GameObject;
            item.transform.SetParent(_btnContainer.transform, false);
            _inventoryItems[i] = item.GetComponent<InventoryItem>();
        }
    }
    public void FilterItems(ItemType type)
    {
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            _inventoryItems[i].transform.SetParent(_unfilteredContaier.transform, false);
            if (_inventoryItems[i].GetItemType() == type)
            {
                filteredItems.Add(_inventoryItems[i]);
            }
        }
        for (int i = 0; i < filteredItems.Count; i++)
        {
            filteredItems[i].transform.SetParent(_btnContainer.transform, false);
        }
    }

    public void EquipItem(InventoryItem itemSelected)
    {
        WizardStatsController.Instance.EquipItem(itemSelected);
    }
}
