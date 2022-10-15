using UnityEngine;
using System.Collections.Generic;

public class WizardStatsController: MonoBehaviour
{
    private WizardStatsData _wizardStatsData;
    public static WizardStatsController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public WizardStatsData GetWizardStatsData()
    {
        if (_wizardStatsData == null)
        {
            _wizardStatsData = LocalStorage.LoadWizardStatsData();
            if (_wizardStatsData == null)
            {
                return new WizardStatsData();
            }
        }
        return LocalStorage.LoadWizardStatsData();
    }

    public void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        LocalStorage.SaveWizardStatsData(wizardStatsData);
        EventManager.Instance.UpdateWizardStats();
    }

    public void ResetData()
    {
        _wizardStatsData = null;
        PlayerStatsController.Instance.ResetData();
        PlayerPrefs.DeleteAll();
    }
    void OnEnable()
    {
        EventManager.Instance.levelUpgrade += UpgardeLevel;
    }
    void OnDisable()
    {
        EventManager.Instance.levelUpgrade -= UpgardeLevel;
    }

    public void UpgardeLevel()
    {
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        _wizardStatsData.BaseAttackStatsData.BaseDamage += 3;
        _wizardStatsData.BaseDefenceStatsData.HP += 3;
        _wizardStatsData.BaseManaStatsData.MaxMana += 2;
        SaveWizardStatsData(_wizardStatsData);
    }
    public void SaveDashboardWizard(DashboardWizard dw)
    {
        SaveWizardStatsData(dw.WizardStatsData);
    }
    
    public void EquipItem(InventoryItem equipedItem)
    {
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        if (equipedItem.GetItemType() == ItemType.Cape)
        {
            _wizardStatsData.CapeStatsData = equipedItem.GetItemStatsData();
        }
        if (equipedItem.GetItemType() == ItemType.Staff)
        {
            _wizardStatsData.StaffStatsData = equipedItem.GetItemStatsData();
        }
        if (equipedItem.GetItemType() == ItemType.Orb)
        {
            _wizardStatsData.OrbStatsData = equipedItem.GetItemStatsData();
        }
        SaveWizardStatsData(_wizardStatsData);
    }
    public void UpdateMasteryPoints(InventoryMastery inventoryMastery)
    {
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        var mastery = FindMastery(_wizardStatsData.MasteriesStatsData, inventoryMastery.GetID());
        if (mastery == null && inventoryMastery.GetCurrentPoints() > 0)
        {
            mastery = new MasteryStatsData(inventoryMastery.GetID());
            mastery.points = inventoryMastery.GetCurrentPoints();
            mastery.maxPoints = inventoryMastery.GetMaxPoints();
            mastery.AttackStatsData = inventoryMastery.AttackStatsData;
            mastery.DefenceStatsData = inventoryMastery.DefenceStatsData;
            mastery.ManaStatsData = inventoryMastery.ManaStatsData;
            _wizardStatsData.MasteriesStatsData.Add(mastery);
        }
        else if (inventoryMastery.GetCurrentPoints() > 0)
        {
            mastery.points = inventoryMastery.GetCurrentPoints();
        }
        else
        {
            _wizardStatsData.MasteriesStatsData.RemoveAll(masteryToRemove => masteryToRemove.name == inventoryMastery.GetID());
        }
        SaveWizardStatsData(_wizardStatsData);
    }
    public MasteryStatsData FindMastery(List<MasteryStatsData> Masteries, string name)
    {
        return Masteries.Find(skill => skill.name.Equals(name));
    }
    public void LearnMagic(string magicName, Magic.MagicType type)
    {
        WizardStatsData _wizardStatsData = GetWizardStatsData();
        if (FindMagic(_wizardStatsData.MagicsStatsData, magicName) == null)
        {
            _wizardStatsData.MagicsStatsData.Add(new MagicStatsData(magicName, type));
        }
        SaveWizardStatsData(_wizardStatsData);
    }
    public MagicStatsData FindMagic(List<MagicStatsData> magicsStatsData, string name)
    {
        return magicsStatsData.Find(magic => magic.name.Equals(name));
    }
}