using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreItem : MonoBehaviour
{
    [SerializeField] float _cost;
    [SerializeField] TMP_Text _costText;
    [SerializeField] int _amount;
    [SerializeField] TMP_Text _amountText;
    [SerializeField] string _description;
    [SerializeField] TMP_Text _descriptionText;
    void Start()
    {
        _costText.text = _cost + "";
        _amountText.text = _amount + "";
        _descriptionText.text = _description;
    }

    public void OnPurchase()
    {
        SoundManager.Instance.PlayButtonSound();
        PlayerStatsData playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        playerStatsData.SetCrystals(playerStatsData.GetCrystals() + _amount);
        PlayerStatsController.Instance.SavePlayerStatsData(playerStatsData);
    }

}
