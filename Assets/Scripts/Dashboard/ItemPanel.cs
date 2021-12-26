using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Image _itemIcon;
    [SerializeField] TMP_Text _nameInput;
    [SerializeField] TMP_Text _requiredLevelInput;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] TMP_Text _itemDescription;
    [SerializeField] TMP_Text[] _attributesInputs = new TMP_Text[4];
    private InventoryItem _itemSelected;

    public void OpenPanel(InventoryItem itemSelected) {
        _itemSelected = itemSelected;
        _itemIcon.sprite = itemSelected.GetIcon();
        _nameInput.text = itemSelected.GetName();
        _itemType.text = itemSelected.GetType();
        _itemDescription.text = itemSelected.GetDescription();
        _requiredLevelInput.text = "Level required: " + itemSelected.GetRequiredLevel().ToString();
        List<string> attributes = itemSelected.GetAttributes();
        for(int i = 0; i < attributes.Count; i++) {
            _attributesInputs[i].text = attributes[i];
        }
        gameObject.SetActive(true);
    }
    
    public void ClosePanel() {
        gameObject.SetActive(false);
    }

    public void EquipItem(){
        var inverntorydata = new InventoryDataController().GetInventoryData();
        InventoryItemData itemDataPointer = _itemSelected.GetInventoryItemData();
        switch (_itemSelected.GetType()) {
            case "Staff":
                inverntorydata.Items.Add(inverntorydata.EquipedStaff);
                inverntorydata.EquipedStaff = itemDataPointer;
                break;
            case "Orb":
                inverntorydata.Items.Add(inverntorydata.EquipedOrb);
                inverntorydata.EquipedOrb = itemDataPointer;
                break;
            case "Cape":
                inverntorydata.Items.Add(inverntorydata.EquipedCape);
                inverntorydata.EquipedCape = itemDataPointer;
                break;
            default:
                break;
        }
        RemoveItemFromList(itemDataPointer, inverntorydata.Items);
        new InventoryDataController().SaveInventoryData(inverntorydata);
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
