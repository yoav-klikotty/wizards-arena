using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MagicModal : MonoBehaviour
{
    [SerializeField] Image _icon;
    private InventoryMagic _inventoryMagic;
    [SerializeField] MagicBook _magicBook;
    [SerializeField] TMP_Text _attributes;
    [SerializeField] TMP_Text _title;
    [SerializeField] Button _learnBtn;
    [SerializeField] TMP_Text _price;
    public void Instantiate(InventoryMagic inventoryMagic)
    {
        _inventoryMagic = inventoryMagic;
        _icon.sprite = inventoryMagic.GetIcon();
        _price.text = inventoryMagic.GetPrice() + "";
        _title.text = inventoryMagic.GetDisplayName();
        RenderAttr();
        if (_inventoryMagic.IsPurchased() || !_inventoryMagic.IsEnable())
        {
           _learnBtn.interactable = false;
        }
        else
        {
            _learnBtn.interactable = true;
        }
    }

    public void RenderAttr()
    {
        _attributes.text = "";
        foreach (var attr in _inventoryMagic.GetAttributes())
        {
            _attributes.text += attr + "\n";
        }

    }
    public void OnLearn()
    {
        _magicBook.LearnMagic(_inventoryMagic);
        gameObject.SetActive(false);
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
