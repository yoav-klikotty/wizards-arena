using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBook : MonoBehaviour
{
    PlayerStatsData playerStatsData;
    WizardStatsData wizardStatsData;
    [SerializeField] List<InventoryMagic> _magics = new List<InventoryMagic>();
    // Start is called before the first frame update
    void Start()
    {
        RefreshBook();
    }
    public void RefreshBook()
    {
        playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        wizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
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
            WizardStatsController.Instance.SaveWizardStatsData(wizardStatsData);
            playerStatsData.SetCoins(playerStatsData.GetCoins() - inventoryMagic.GetPrice());
            PlayerStatsController.Instance.SavePlayerStatsData(playerStatsData);
            RefreshBook();
        }

    }
}
