using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public List<String> EquipedItems = new List<string>();
    public List<String> Items = new List<string>();
}

// [Serializable]
// public class InventoryItemData
// {
//     public string name = new List<string>();
//     public List<String> Gems = new List<string>();
// }