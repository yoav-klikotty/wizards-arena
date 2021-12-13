using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    
    [SerializeField] string _name;
    [SerializeField] int _requiredLevel;
    [SerializeField] string[] _attributes = new string[4];
    [SerializeField] string _description;
    [SerializeField] string _type;
    private ItemPanel _itemPanel;
    private Sprite _icon;
    
    void Start(){
        _icon = GetComponent<Image>().sprite;
        _itemPanel = Resources.FindObjectsOfTypeAll<ItemPanel>()[0];
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
    public string[] GetAttributes() {
        return _attributes;
    }
}