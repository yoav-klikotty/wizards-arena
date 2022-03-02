using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WizardStatsData
{
    public ItemStatsData StaffStatsData;
    public ItemStatsData CapeStatsData;
    public ItemStatsData OrbStatsData;
    public AttackStatsData BaseAttackStatsData;
    public DefenceStatsData BaseDefenceStatsData;
    public ManaStatsData BaseManaStatsData;
    public List<MagicStatsData> MagicsStatsData = new List<MagicStatsData> {
        new MagicStatsData("MagicChargeBlue"),
        new MagicStatsData("MagicShieldBlue"),
        new MagicStatsData("BlueMissile"),
        new MagicStatsData("ElectricShot"),
        new MagicStatsData("PurpleLightning"),
    };
    public List<MasteryStatsData> MasteriesStatsData = new List<MasteryStatsData>
    {
    };
    public void EquipItem(InventoryItem equipedItem)
    {
        if (equipedItem.GetItemType() == ItemType.Cape)
        {
            this.CapeStatsData = equipedItem.GetItemStatsData();
        }
        if (equipedItem.GetItemType() == ItemType.Staff)
        {
            this.StaffStatsData = equipedItem.GetItemStatsData();
        }
        if (equipedItem.GetItemType() == ItemType.Orb)
        {
            this.OrbStatsData = equipedItem.GetItemStatsData();
        }
    }

    public void UpdateMasteryPoints(InventoryMastery inventoryMastery)
    {
        var mastery = FindMastery(inventoryMastery.GetID());
        if (mastery == null && inventoryMastery.GetCurrentPoints() > 0)
        {
            mastery = new MasteryStatsData(inventoryMastery.GetID());
            mastery.points = inventoryMastery.GetCurrentPoints();
            mastery.maxPoints = inventoryMastery.GetMaxPoints();
            mastery.AttackStatsData = inventoryMastery.AttackStatsData;
            mastery.DefenceStatsData = inventoryMastery.DefenceStatsData;
            mastery.ManaStatsData = inventoryMastery.ManaStatsData;
            MasteriesStatsData.Add(mastery);
        }
        else if (inventoryMastery.GetCurrentPoints() > 0)
        {
            mastery.points = inventoryMastery.GetCurrentPoints();
        }
        else
        {
            MasteriesStatsData.RemoveAll(masteryToRemove => masteryToRemove.name == inventoryMastery.GetID());
        }
    }
    public MasteryStatsData FindMastery(string name)
    {
        return MasteriesStatsData.Find(skill => skill.name.Equals(name));
    }
    public int GetTotalBaseDamage()
    {
        int masteriesBaseDamage = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesBaseDamage += masteriesStatsData.points * masteriesStatsData.AttackStatsData.BaseDamage;
        }
        return (BaseAttackStatsData.BaseDamage +
                CapeStatsData.AttackStatsData.BaseDamage +
                OrbStatsData.AttackStatsData.BaseDamage +
                StaffStatsData.AttackStatsData.BaseDamage +
                masteriesBaseDamage
                );
    }
    public float GetTotalCriticalDmg()
    {
        float smasteriesCritDamage = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            smasteriesCritDamage += masteriesStatsData.points * masteriesStatsData.AttackStatsData.CriticalDmg;
        }
        return (BaseAttackStatsData.CriticalDmg +
                CapeStatsData.AttackStatsData.CriticalDmg +
                OrbStatsData.AttackStatsData.CriticalDmg +
                StaffStatsData.AttackStatsData.CriticalDmg +
                smasteriesCritDamage
                );
    }

    public float GetTotalCriticalRate()
    {
        float masteriesCritRate = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesCritRate += masteriesStatsData.points * masteriesStatsData.AttackStatsData.CriticalRate;
        }
        return (BaseAttackStatsData.CriticalRate +
                CapeStatsData.AttackStatsData.CriticalRate +
                OrbStatsData.AttackStatsData.CriticalRate +
                StaffStatsData.AttackStatsData.CriticalRate + 
                masteriesCritRate
                );
    }
    public float GetTotalArmorPenetration()
    {
        float masteriesArmorPenetration = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesArmorPenetration += masteriesStatsData.points * masteriesStatsData.AttackStatsData.ArmorPenetration;
        }
        return (BaseAttackStatsData.ArmorPenetration +
                CapeStatsData.AttackStatsData.ArmorPenetration +
                OrbStatsData.AttackStatsData.ArmorPenetration +
                StaffStatsData.AttackStatsData.ArmorPenetration +
                masteriesArmorPenetration
                );
    }
    public int GetTotalHP()
    {
        int masteriesHP = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesHP += masteriesStatsData.points * masteriesStatsData.DefenceStatsData.HP;
        }
        return (BaseDefenceStatsData.HP +
                CapeStatsData.DefenceStatsData.HP +
                OrbStatsData.DefenceStatsData.HP +
                StaffStatsData.DefenceStatsData.HP + 
                masteriesHP
                );
    }
    public int GetTotalRecovery()
    {
        int masteriesRecovery = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesRecovery += masteriesStatsData.points * masteriesStatsData.DefenceStatsData.Recovery;
        }
        return (BaseDefenceStatsData.Recovery +
                CapeStatsData.DefenceStatsData.Recovery +
                OrbStatsData.DefenceStatsData.Recovery +
                StaffStatsData.DefenceStatsData.Recovery + 
                masteriesRecovery
                );
    }
    public float GetTotalAvoidability()
    {
        float masteriesAvoidability = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesAvoidability += masteriesStatsData.points * masteriesStatsData.DefenceStatsData.Avoidability;
        }
        return (BaseDefenceStatsData.Avoidability +
                CapeStatsData.DefenceStatsData.Avoidability +
                OrbStatsData.DefenceStatsData.Avoidability +
                StaffStatsData.DefenceStatsData.Avoidability + 
                masteriesAvoidability
                );
    }
    public int GetTotalMaxMana()
    {
        int masteriesMaxMana = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesMaxMana += masteriesStatsData.points * masteriesStatsData.ManaStatsData.MaxMana;
        }
        return (BaseManaStatsData.MaxMana +
                CapeStatsData.ManaStatsData.MaxMana +
                OrbStatsData.ManaStatsData.MaxMana +
                StaffStatsData.ManaStatsData.MaxMana + 
                masteriesMaxMana
                );
    }
    public int GetTotalManaRegeneration()
    {
        int masteriesManaRegeneration = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesManaRegeneration += masteriesStatsData.points * masteriesStatsData.ManaStatsData.ManaRegeneration;
        }
        return (BaseManaStatsData.ManaRegeneration +
                CapeStatsData.ManaStatsData.ManaRegeneration +
                OrbStatsData.ManaStatsData.ManaRegeneration +
                StaffStatsData.ManaStatsData.ManaRegeneration +
                masteriesManaRegeneration
                );
    }
    public int GetTotalPassiveManaRegeneration()
    {
        int masteriesPassive = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesPassive += masteriesStatsData.points * masteriesStatsData.ManaStatsData.PassiveManaRegeneration;
        }
        return (BaseManaStatsData.PassiveManaRegeneration +
                CapeStatsData.ManaStatsData.PassiveManaRegeneration +
                OrbStatsData.ManaStatsData.PassiveManaRegeneration +
                StaffStatsData.ManaStatsData.PassiveManaRegeneration +
                masteriesPassive
                );
    }
    public int GetTotalStartMana()
    {
        int masteriesStartMana = 0;
        foreach (var masteriesStatsData in MasteriesStatsData)
        {
            masteriesStartMana += masteriesStatsData.points * masteriesStatsData.ManaStatsData.StartMana;
        }
        return (BaseManaStatsData.StartMana +
                CapeStatsData.ManaStatsData.StartMana +
                OrbStatsData.ManaStatsData.StartMana +
                StaffStatsData.ManaStatsData.StartMana +
                masteriesStartMana
                );
    }
}

[Serializable]
public class AttackStatsData
{
    public int BaseDamage = 3;
    public float CriticalRate = 0.1f;
    public float CriticalDmg = 0.1f;
    public float ArmorPenetration = 0;
}

[Serializable]
public class DefenceStatsData
{
    public int HP = 25;
    public int Recovery = 0;
    public float Avoidability = 0;
}

[Serializable]
public class ManaStatsData
{
    public int MaxMana = 20;
    public int StartMana = 0;
    public int ManaRegeneration = 3;
    public int PassiveManaRegeneration = 0;
}

[Serializable]
public class MagicStatsData
{
    public string name;

    public MagicStatsData(string name)
    {
        this.name = name;
    }
}

[Serializable]
public class MasteryStatsData
{
    public string name;
    public int points;
    public int maxPoints;
    public DefenceStatsData DefenceStatsData;
    public AttackStatsData AttackStatsData;
    public ManaStatsData ManaStatsData;
    public MasteryStatsData(string name)
    {
        this.name = name;
    }
}
[Serializable]
public class ItemStatsData
{
    public string Name;
    public List<string> materials;
    public DefenceStatsData DefenceStatsData;
    public AttackStatsData AttackStatsData;
    public ManaStatsData ManaStatsData;
    public Material[] GetMaterials()
    {
        return new Material[] {
            Resources.Load<Material>("Materials/" + materials[0]),
        };
    }
    public bool IsContainInventoryItem(InventoryItem inventoryItem)
    {
        return Name.Equals(inventoryItem.GetName());
    }
}