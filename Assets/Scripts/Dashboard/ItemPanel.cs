using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Image _itemIcon;
    [SerializeField] Button _actionButton;
    [SerializeField] TMP_Text _actionButtonText;
    [SerializeField] TMP_Text _nameInput;
    [SerializeField] TMP_Text _priceText;
    [SerializeField] GameObject _price;
    [SerializeField] TMP_Text _requiredLevelInput;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] TMP_Text _itemDescription;
    [SerializeField] TMP_Text[] _attributesInputs = new TMP_Text[4];
    [SerializeField] InventoryManager _inventoryManager;
    private InventoryItem _itemSelected;
    private bool _isPlayerLevelSufficient;
    private bool _isPlayerFundsSufficient;
    private bool _isPlayerPurchasedItem;
    private bool _isPlayerEquipedItem;
    private PlayerStatsController _playerStatsController = new PlayerStatsController();

    public void OpenPanel(InventoryItem itemSelected)
    {
        PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
        _itemSelected = itemSelected;
        _itemIcon.sprite = itemSelected.GetIcon();
        _nameInput.text = itemSelected.GetDisplayName();
        _itemType.text = itemSelected.GetItemType().ToString();
        _itemDescription.text = itemSelected.GetDescription();
        _requiredLevelInput.text = "Level required: " + itemSelected.GetRequiredLevel().ToString();
        _isPlayerLevelSufficient = itemSelected.GetRequiredLevel() <= playerStatsData.GetLevel();
        _isPlayerFundsSufficient = itemSelected.GetPrice() <= playerStatsData.GetCoins();
        _isPlayerPurchasedItem = playerStatsData.IsPurchasedItem(itemSelected.GetName());
        _isPlayerEquipedItem = itemSelected.GetEquipedStatus();
        List<string> attributes = itemSelected.GetAttributes();
        for (int i = 0; i < _attributesInputs.Length; i++)
        {
            _attributesInputs[i].text = "";
        }
        for (int i = 0; i < attributes.Count; i++)
        {
            _attributesInputs[i].text = attributes[i];
        }
        RenderActionButton();
        RenderPrice();
        gameObject.SetActive(true);
    }

    private void RenderPrice()
    {
        if (!_isPlayerPurchasedItem)
        {
            _price.SetActive(true);
            _priceText.text = _itemSelected.GetPrice() + "";
        }
        else 
        {
            _price.SetActive(false);
        }
    }

    private void RenderActionButton()
    {
        if (!_isPlayerPurchasedItem)
        {
            _actionButtonText.text = "Buy";
            if (!_isPlayerLevelSufficient || !_isPlayerFundsSufficient)
            {
                _actionButton.interactable = false;
            }
            else
            {
                _actionButton.interactable = true;
            }

        }
        else
        {
            _actionButtonText.text = "Equip";
            if (_isPlayerEquipedItem)
            {
                _actionButton.interactable = false;
            }
            else
            {
                _actionButton.interactable = true;
            }

        }

    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void HandleActionButton()
    {
        if (!_isPlayerPurchasedItem)
        {
            _inventoryManager.PurchaseItem(_itemSelected);
        }
        else
        {
            _inventoryManager.EquipItem(_itemSelected);
        }
        gameObject.SetActive(false);
    }
}
