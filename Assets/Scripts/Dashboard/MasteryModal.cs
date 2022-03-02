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
    [SerializeField] MasteryTreeManager _masteryTreeManager;
    [SerializeField] TMP_Text _masteryAttributes;
    public void Instantiate(InventoryMastery inventoryMastery)
    {
        _inventoryMastery = inventoryMastery;
        _icon.sprite = _inventoryMastery.GetIcon();
        _initialPoints = _inventoryMastery.GetCurrentPoints();
        RenderMasteriesAttr();
    }

    public void RenderMasteriesAttr()
    {
        _masteryAttributes.text = "";
        foreach (var attr in _inventoryMastery.GetAttributes())
        {
            _masteryAttributes.text += attr + "\n";
        }

    }
    public void OnUpgrade()
    {
        if (_inventoryMastery.GetCurrentPoints() < _inventoryMastery.GetMaxPoints())
        {
            _masteryTreeManager.UpdateSkill(_inventoryMastery);
            gameObject.SetActive(false);
        }
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
