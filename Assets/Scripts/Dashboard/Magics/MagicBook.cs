using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBook : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    PlayerStatsData playerStatsData;
    private WizardStatsController _wizardStatsController = new WizardStatsController();
    WizardStatsData wizardStatsData;
    [SerializeField] List<InventoryMagic> _magics = new List<InventoryMagic>();
    // Start is called before the first frame update
    void Start()
    {
        RefreshBook();
    }
    public void RefreshBook()
    {
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        wizardStatsData = _wizardStatsController.GetWizardStatsData();
        foreach (var inventoryMagic in _magics)
        {
            if (wizardStatsData.FindMagic(inventoryMagic.GetID()) != null)
            {
                inventoryMagic.EnableState(true);
                inventoryMagic.PurchasedState(true);
            }
            else if(inventoryMagic.GetRequiredLevel() <= playerStatsData.GetLevel() && inventoryMagic.GetPrice() <= playerStatsData.GetCoins())
            {
                inventoryMagic.EnableState(true);
            }
            else
            {
                inventoryMagic.EnableState(false);
                inventoryMagic.PurchasedState(false);
            }
        }

    }
    public void LearnMagic(InventoryMagic inventoryMagic)
    {
        if (playerStatsData.GetCoins() > inventoryMagic.GetPrice())
        {
            wizardStatsData.LearnMagic(inventoryMagic.GetID(), inventoryMagic.GetMagicType());
            _wizardStatsController.SaveWizardStatsData(wizardStatsData);
            playerStatsData.SetCoins(playerStatsData.GetCoins() - inventoryMagic.GetPrice());
            _playerStatsController.SavePlayerStatsData(playerStatsData);
            RefreshBook();
        }

    }
}
