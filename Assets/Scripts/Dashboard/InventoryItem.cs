using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData _itemData;
    private string _name;
    private int _requiredLevel;
    private List<string> _attributes = new List<string>();
    private string _description;
    private int _numOfSockets;
    private string _type;
    private bool _equiped;
    private Sprite _icon;
    private ItemPanel _itemPanel;
    private InventoryItemData _inventoryItemData;
    [SerializeField] string _gems;

    void Awake() {
        _type = "Cape";
    }
    
    void Start(){
        _icon = GetComponent<Image>().sprite;
        _itemPanel = Resources.FindObjectsOfTypeAll<ItemPanel>()[0];
    }
    public void SetData(InventoryItemData itemDataPointer) {
        _name = _itemData.Name;
        _requiredLevel = _itemData.RequiredLevel;
        _numOfSockets = _itemData.NumOfSockets;
        _type = _itemData.Type;
        _description = _itemData.Description;
        _attributes = _itemData.GetAttributes();
        _inventoryItemData = itemDataPointer;
    }
    public void ItemClicked(){
        _itemPanel.OpenPanel(this);
        RectTransform itemRect = this.GetComponent<RectTransform>();
    }
    public string GetName() {
        return _name;
    }
    public int GetRequiredLevel() {
        return _requiredLevel;
    }
    public string GetDescription() {
        return _description;
    }
    public string GetType() {
        return _type;
    }
    public Sprite GetIcon() {
        return _icon;
    }
    public List<string> GetAttributes() {
        return _attributes;
    }

    public InventoryItemData GetInventoryItemData() {
        return _inventoryItemData;
    }
}
