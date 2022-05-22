using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject _unfilteredContaier;
    [SerializeField] Wizard _wizard;
    [SerializeField] GameObject _btnContainer;
    private InventoryItem[] _inventoryItems;
    [SerializeField] WizardStats _wizardStats;
    private InventoryPrefabs[] totalItems = new InventoryPrefabs[] {
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
            new InventoryPrefabs("Cape_Default", ItemType.Cape),
            new InventoryPrefabs("Orb_Default", ItemType.Orb),
            new InventoryPrefabs("Staff_Default", ItemType.Staff),
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
        _wizardStats.WriteWizardStats();
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
        WizardStatsData wizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
        wizardStatsData.EquipItem(itemSelected);
        WizardStatsController.Instance.SaveWizardStatsData(wizardStatsData);
        _wizard.UpdateWizard(wizardStatsData);
        _wizardStats.WriteWizardStats();
    }
}
