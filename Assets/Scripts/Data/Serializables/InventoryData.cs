using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public InventoryItemData EquipedStaff = new InventoryItemData();
    public InventoryItemData EquipedOrb = new InventoryItemData();
    public InventoryItemData EquipedCape = new InventoryItemData();
    public List<InventoryItemData> Items = new List<InventoryItemData>();
}

[Serializable]
public class InventoryItemData
{
    public string Name;
    public List<String> Gems = new List<string>();
}
