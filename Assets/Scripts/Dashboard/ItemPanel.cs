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
        string[] attributes = itemSelected.GetAttributes();
        for(int i = 0; i < _attributesInputs.Length; i++) {
            _attributesInputs[i].text = attributes[i];
        }
        gameObject.SetActive(true);
    }
    
    public void ClosePanel() {
        gameObject.SetActive(false);
    }

    public void EquipItem(){
    //     var inverntorydata = new InventoryDataController().GetInventoryData();
    //     switch (_itemSelected.GetType()) {
    //         case "Staff":
    //             inverntorydata.Items.Add(inverntorydata.EquipedStaff);
    //             inverntorydata.EquipedStaff = _itemSelected.GetName();
    //             break;
    //         case "Orb":
    //             inverntorydata.Items.Add(inverntorydata.EquipedOrb);
    //             inverntorydata.EquipedOrb = _itemSelected.GetName();
    //             break;
    //         case "Cape":
    //             inverntorydata.Items.Add(inverntorydata.EquipedCape);
    //             inverntorydata.EquipedCape = _itemSelected.GetName();
    //             break;
    //         default:
    //             break;
    //     }
    //     new InventoryDataController().SaveInventoryData(inverntorydata);
    //     gameObject.SetActive(false);
    }
}
