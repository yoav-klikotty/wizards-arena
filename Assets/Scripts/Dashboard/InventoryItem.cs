using System.Collections;
using System.Collections.Generic;
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
    private bool _equiped;
    [SerializeField] ItemType _type;
    [SerializeField] string _name;
    [SerializeField] string _displayName;

    [SerializeField] int _requiredLevel;
    [SerializeField] List<string> _attributes = new List<string>();
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemPanel _itemPanel;
    [SerializeField] DefenceStatsData _defenceStatsData;
    [SerializeField] AttackStatsData _attackStatsData;
    [SerializeField] ManaStatsData _manaStatsData;
    [SerializeField] ItemStatsData _itemStatsData;

    void Start()
    {
        _icon = GetComponent<Image>().sprite;
        _itemPanel = Resources.FindObjectsOfTypeAll<ItemPanel>()[0];
    }
    public void SetEquipedStatus(bool equiped)
    {
        _equiped = equiped;
    }
    public bool GetEquipedStatus()
    {
        return _equiped;
    }
    public void ItemClicked()
    {
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
    public int GetRequiredLevel()
    {
        return _requiredLevel;
    }
    public string GetDescription()
    {
        return _description;
    }
    public ItemType GetItemType()
    {
        return _type;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public List<string> GetAttributes()
    {
        return _attributes;
    }
    public AttackStatsData GetAttackStatsData()
    {
        return _attackStatsData;
    }
    public DefenceStatsData GetDefenceStatsData()
    {
        return _defenceStatsData;
    }
    public ManaStatsData GetManaStatsData()
    {
        return _manaStatsData;
    }
    public ItemStatsData GetItemStatsData()
    {
        return _itemStatsData;
    }
}
