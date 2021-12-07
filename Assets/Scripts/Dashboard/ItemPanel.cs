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
    public void OpenPanel(Sprite sprite, string name, int requiredLevel, string[] attributes, string description, string type) {
        _itemIcon.sprite = sprite;
        _nameInput.text = name;
        _itemType.text = type;
        _itemDescription.text = description;
        _requiredLevelInput.text = "Level required: " + requiredLevel.ToString();
        for(int i = 0; i < _attributesInputs.Length; i++) {
            _attributesInputs[i].text = attributes[i];
        }
        gameObject.SetActive(true);
    }
    
    public void ClosePanel() {
        gameObject.SetActive(false);
    }
}
