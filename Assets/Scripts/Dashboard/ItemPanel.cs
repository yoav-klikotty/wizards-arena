using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Button _actionButton;
    [SerializeField] TMP_Text _priceText;
    [SerializeField] TMP_Text _requiredLevelInput;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] WizardStats _wizardStats;
    private InventoryItem _itemSelected;
    private bool _isPlayerLevelSufficient;
    private bool _isPlayerFundsSufficient;
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    PlayerStatsData playerStatsData;
    WizardStatsData wizardStatsData;
    public void OpenPanel(InventoryItem itemSelected)
    {
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        wizardStatsData = _wizardStatsController.GetWizardStatsData();
        _itemSelected = itemSelected;
        _itemType.text = itemSelected.GetItemType().ToString();
        _requiredLevelInput.text = "Level required: " + itemSelected.GetRequiredLevel().ToString();
        _isPlayerLevelSufficient = itemSelected.GetRequiredLevel() <= playerStatsData.GetLevel();
        _isPlayerFundsSufficient = itemSelected.GetPrice() <= playerStatsData.GetCoins();
        RenderPrice();
        RenderActionButton();
        gameObject.SetActive(true);
        if (itemSelected.GetItemType() == ItemType.Cape)
        {
            _wizardStats.SetAllDiff(_itemSelected, wizardStatsData.CapeStatsData.AttackStatsData, wizardStatsData.CapeStatsData.DefenceStatsData, wizardStatsData.CapeStatsData.ManaStatsData);
        }
        if (itemSelected.GetItemType() == ItemType.Orb)
        {
            _wizardStats.SetAllDiff(_itemSelected, wizardStatsData.OrbStatsData.AttackStatsData, wizardStatsData.OrbStatsData.DefenceStatsData, wizardStatsData.OrbStatsData.ManaStatsData);
        }
        if (itemSelected.GetItemType() == ItemType.Staff)
        {
            _wizardStats.SetAllDiff(_itemSelected, wizardStatsData.StaffStatsData.AttackStatsData, wizardStatsData.StaffStatsData.DefenceStatsData, wizardStatsData.StaffStatsData.ManaStatsData);
        }
    }
    private bool IsInventoryItemEquiped()
    {
        if (_itemSelected.GetItemType() == ItemType.Cape)
        {
            return wizardStatsData.CapeStatsData.Name.Equals(_itemSelected.GetName());
        }
        if (_itemSelected.GetItemType() == ItemType.Staff)
        {
            return wizardStatsData.StaffStatsData.Name.Equals(_itemSelected.GetName());
        }
        if (_itemSelected.GetItemType() == ItemType.Orb)
        {
            return wizardStatsData.OrbStatsData.Name.Equals(_itemSelected.GetName());
        }
        return true;
    }

    private void RenderPrice()
    {
        _priceText.text = _itemSelected.GetPrice() + "";
    }

    public void ClosePanel()
    {
        _wizardStats.RemoveAllDiff();
        gameObject.SetActive(false);
    }

    public void HandleActionButton()
    {
        _inventoryManager.EquipItem(_itemSelected);
        playerStatsData.SetCoins(playerStatsData.GetCoins() - _itemSelected.GetPrice());
        _playerStatsController.SavePlayerStatsData(playerStatsData, true);
        gameObject.SetActive(false);
    }

    public void RenderActionButton()
    {
        if (_isPlayerLevelSufficient && _isPlayerLevelSufficient && !IsInventoryItemEquiped())
        {
            _actionButton.gameObject.SetActive(true);
        }
        else
        {
            _actionButton.gameObject.SetActive(false);
        }
    }
}
