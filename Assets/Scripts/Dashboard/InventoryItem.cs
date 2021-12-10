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
        _itemPanel.OpenPanel(_icon, _name, _requiredLevel, _attributes, _description, _type);
        RectTransform itemRect = this.GetComponent<RectTransform>();
    }
}
