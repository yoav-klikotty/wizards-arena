using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMagic : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] string _displayName;
    [SerializeField] Sprite _icon;
    [SerializeField] List<string> _attributes;
    [SerializeField] GameObject _purchaseIcon;
    [SerializeField] int _requiredMana;
    [SerializeField] Magic.MagicType _magicType;
    [SerializeField] int _requiredLevel;
    [SerializeField] int _price;
    [SerializeField] MagicModal _magicModal;
    private bool _purchased;
    private bool _enable;

    public string GetID()
    {
        return _name;
    }

    public string GetDisplayName()
    {
        return _displayName;
    }

    public int GetRequiredMana()
    {
        return _requiredMana;
    }

    public Sprite GetIcon()
    {
        return _icon;
    }
    public void OnClick()
    {
        _magicModal.gameObject.SetActive(true);
        _magicModal.Instantiate(this);
    }
    public List<string> GetAttributes()
    {
        return _attributes;
    }

    public int GetPrice()
    {
        return _price;
    }
    public int GetRequiredLevel()
    {
        return _requiredLevel;
    }
    public bool IsPurchased()
    {
        return _purchased;
    }
    public bool IsEnable()
    {
        return this._enable;
    }
    public void PurchasedState(bool isPurchased)
    {
        this._purchased = isPurchased;
        _purchaseIcon.SetActive(!isPurchased);
    }
    public void EnableState(bool isEnable)
    {
        this._enable = isEnable;
        GetComponent<Button>().interactable = isEnable;
    }
}
