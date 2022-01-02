using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Image _itemIcon;
    [SerializeField] Button _equipButton;
    [SerializeField] TMP_Text _nameInput;
    [SerializeField] TMP_Text _requiredLevelInput;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] TMP_Text _itemDescription;
    [SerializeField] TMP_Text[] _attributesInputs = new TMP_Text[4];
    [SerializeField] InventoryManeger _inventoryManager;
    private InventoryItem _itemSelected;
    

    public void OpenPanel(InventoryItem itemSelected) {
        _itemSelected = itemSelected;
        _itemIcon.sprite = itemSelected.GetIcon();
        _nameInput.text = itemSelected.GetName();
        _itemType.text = itemSelected.GetType();
        _itemDescription.text = itemSelected.GetDescription();
        _requiredLevelInput.text = "Level required: " + itemSelected.GetRequiredLevel().ToString();
        List<string> attributes = itemSelected.GetAttributes();
        _equipButton.interactable = !itemSelected.GetEquipedStatus();
        for(int i = 0; i < attributes.Count; i++) {
            _attributesInputs[i].text = attributes[i];
        }
        gameObject.SetActive(true);
    }
    
    public void ClosePanel() {
        gameObject.SetActive(false);
    }

    public void EquipItem(){
        _inventoryManager.EquipItem(_itemSelected);
        gameObject.SetActive(false);
    }

    private void RemoveItemFromList(InventoryItemData itemEquiped, List<InventoryItemData> unequipedItems) {
        for(int i = 0; i < unequipedItems.Count; i++) {
            if(itemEquiped == unequipedItems[i]) {
                unequipedItems.RemoveAt(i);
            }
        }
    }
}
