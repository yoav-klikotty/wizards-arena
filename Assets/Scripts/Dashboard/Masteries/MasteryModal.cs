using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasteryModal : MonoBehaviour
{
    [SerializeField] Image _icon;
    private InventoryMastery _inventoryMastery;
    private int _initialPoints;
    [SerializeField] TMP_Text _masteryAttributes;
    [SerializeField] TMP_Text _title;
    [SerializeField] MasteryTree masteryTree;
    [SerializeField] List<Level> _levels;
    [SerializeField] GameObject _message;
    [SerializeField] Button _unlock;

    public void Instantiate(InventoryMastery inventoryMastery)
    {
        _inventoryMastery = inventoryMastery;
        _icon.sprite = _inventoryMastery.GetIcon();
        _initialPoints = _inventoryMastery.GetCurrentPoints();
        _title.text = inventoryMastery.GetDisplayName();
        CheckPlayerXP();
        RenderMasteriesAttr();
        RenderLevels();
    }
    private void CheckPlayerXP()
    {
        if (masteryTree.playerStatsData.GetMasteriesPoints() == 0)
        {
            _message.SetActive(true);
            _unlock.interactable = false;
        }
        else
        {
            _message.SetActive(false);
            _unlock.interactable = true;
        }
    }

    public void RenderMasteriesAttr()
    {
        _masteryAttributes.text = "";
        foreach (var attr in _inventoryMastery.GetAttributes())
        {
            _masteryAttributes.text += attr + "\n";
        }

    }
    public void RenderLevels()
    {
        for (var i = 0 ; i < _inventoryMastery.GetCurrentPoints(); i++)
        {
            _levels[i].Active(true);
        }
        for (var i = _inventoryMastery.GetCurrentPoints(); i < 5; i++)
        {
            _levels[i].Active(false);
        }
    }
    public void OnUpgrade()
    {
        SoundManager.Instance.PlayMasteryUpgradeSound();
        if (_inventoryMastery.GetCurrentPoints() < _inventoryMastery.GetMaxPoints())
        {
            masteryTree.UpdateSkill(_inventoryMastery);
            gameObject.SetActive(false);
        }
    }
    public void OnExit()
    {
        SoundManager.Instance.PlayNegativeButtonSound();
        gameObject.SetActive(false);
    }
}
