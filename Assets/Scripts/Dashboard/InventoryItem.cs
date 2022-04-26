using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Cape,
    Staff,
    Orb,
    Other,
}


public class InventoryPrefabs
{
    public string Name;
    public ItemType Type;

    public InventoryPrefabs(string name, ItemType type)
    {
        this.Name = name;
        this.Type = type;
    }
}

public class InventoryItem : MonoBehaviour
{
    [SerializeField] ItemType _type;
    [SerializeField] string _name;
    [SerializeField] string _displayName;
    [SerializeField] int _price;
    [SerializeField] int _requiredLevel;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemPanel _itemPanel;
    [SerializeField] ItemStatsData _itemStatsData;

    void Start()
    {
        _icon = GetComponent<Image>().sprite;
        _itemStatsData.Name = _name;
        _itemPanel = Resources.FindObjectsOfTypeAll<ItemPanel>()[0];
    }
    public void ItemClicked()
    {
        SoundManager.Instance.PlayButtonSound();
        _itemPanel.OpenPanel(this);
        RectTransform itemRect = this.GetComponent<RectTransform>();
    }
    public string GetName()
    {
        return _name;
    }
    public string GetDisplayName()
    {
        return _displayName;
    }
    public int GetPrice()
    {
        return _price;
    }
    public int GetRequiredLevel()
    {
        return _requiredLevel;
    }
    public ItemType GetItemType()
    {
        return _type;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public ItemStatsData GetItemStatsData()
    {
        return _itemStatsData;
    }
}
