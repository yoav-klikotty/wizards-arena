using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public List<InventoryItemData> Items = new List<InventoryItemData>();
}

[Serializable]
public class InventoryItemData
{
    public string Name;
    public bool Equiped;
}
