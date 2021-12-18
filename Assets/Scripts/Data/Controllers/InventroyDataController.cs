using UnityEngine;

public class InventoryDataController
{
    private InventoryData InventoryData; 
    public InventoryData GetInventoryData()
    {
        if (InventoryData == null)
        {
            InventoryData = LocalStorage.LoadInventoryData();
        }
        return InventoryData;
    }

    public void SaveInventoryData(InventoryData InventoryData)
    {
        LocalStorage.SaveInventoryData(InventoryData);
    }
}
