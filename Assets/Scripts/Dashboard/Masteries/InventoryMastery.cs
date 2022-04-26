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
    [SerializeField] List<InventoryMastery> _dependenciesMasteries = new List<InventoryMastery>();
    [SerializeField] List<InventoryMastery> _excludeMasteries = new List<InventoryMastery>();
    [SerializeField] GameObject _plusIcon;
    /**
    States: 1. Not valid - since not in level or previous dependicies not opened.
            2. Valid 
            3. Full
    **/
    public void Validate(WizardStatsData wizardStatsData, PlayerStatsData playerStatsData)
    {
        if (!IsLevelSufficient(playerStatsData._level))
        {
            RenderLevelNotSufficient();
        }
        else if (!IsExcludedValid())
        {
            RenderLevelNotSufficient();
        }
        else if (!IsDependeciesValid())
        {
            RenderLevelNotSufficient();
        }
        else
        {
            EnableMastery();
            var mastery = wizardStatsData.FindMastery(GetID());
            if (mastery == null)
            {
                _plusIcon.SetActive(true);
            }
            else
            {
                if (mastery.points > 0)
                {
                    SetCurrentPoints(mastery.points);
                }
                if (_currentPoints == _maxPoints)
                {
                    _plusIcon.SetActive(false);
                }
                else
                {
                    _plusIcon.SetActive(true);
                }
            }
        }
    }

    public bool IsLevelSufficient(int playerLevel)
    {
        return playerLevel >= GetRequiredLevel();
    }
    public bool IsDependeciesValid()
    {
        if (_dependenciesMasteries.Count == 0) return true;
        foreach (var dependency in _dependenciesMasteries)
        {
            if (dependency._currentPoints > 0)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsExcludedValid()
    {
        if (_excludeMasteries.Count == 0) return true;
        foreach (var excludedMastery in _excludeMasteries)
        {
            if (excludedMastery._currentPoints == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void RenderLevelNotSufficient()
    {
        _plusIcon.SetActive(false);
        DisableMastery();
    }

    public void RenderValidState()
    {
        DisableMastery();
    }

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
        SoundManager.Instance.PlayButtonSound();
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
    public string GetDisplayName()
    {
        return _displayName;
    }

}
