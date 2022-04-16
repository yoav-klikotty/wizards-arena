using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventoryMasteries
{
    public List<InventoryMastery> masteries;
}

[System.Serializable]
public class InventoryMastery : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] string _displayName;
    [SerializeField] List<string> _attributes;
    [SerializeField] int _maxPoints;
    private int _currentPoints;
    [SerializeField] int _requiredLevel;
    [SerializeField] Button _masteryBtn;
    public DefenceStatsData DefenceStatsData;
    public AttackStatsData AttackStatsData;
    public ManaStatsData ManaStatsData;
    [SerializeField] Sprite _icon;
    [SerializeField] MasteryModal _masteryModal;

    public string GetID()
    {
        return _name;
    }
    public void SetCurrentPoints(int currentPoints)
    {
        _currentPoints = currentPoints;
    }
    public int GetRequiredLevel()
    {
        return _requiredLevel;
    }
    public void EnableMastery()
    {
        _masteryBtn.interactable = true;
    }
    public void DisableMastery()
    {
        _masteryBtn.interactable = false;
    }
    public int GetCurrentPoints()
    {
        return _currentPoints;
    }
    public int GetMaxPoints()
    {
        return _maxPoints;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public void OnClick()
    {
        if (GetCurrentPoints() < GetMaxPoints())
        {
            _masteryModal.gameObject.SetActive(true);
            _masteryModal.Instantiate(this);
        }
    }

    public List<string> GetAttributes()
    {
        return _attributes;
    }

}
