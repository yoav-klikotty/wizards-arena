using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WizardStatsData
{
    public StaffStatsData StaffStatsData;
    public AttackStatsData AttackStatsData;
    public CapeStatsData CapeStatsData;
    public DefenceStatsData DefenceStatsData;
    public OrbStatsData OrbStatsData;
    public ManaStatsData ManaStatsData;

    public void WriteWizardStats(){
        WizardStats wizardStatsObject = GameObject.Find("WizardStats").GetComponent<WizardStats>();
        wizardStatsObject.SetDmg(AttackStatsData.MinBaseDamage, AttackStatsData.MaxBaseDamage);
        wizardStatsObject.SetCritDmg(AttackStatsData.CriticalDmg);
        wizardStatsObject.SetCritRate(AttackStatsData.CriticalRate);
        wizardStatsObject.SetArmorPenetration(AttackStatsData.ArmorPenetration);
        wizardStatsObject.SetMaxHP(DefenceStatsData.MaxHP);
        wizardStatsObject.SetRecovery(DefenceStatsData.Recovery);
        wizardStatsObject.SetMirroring(DefenceStatsData.Mirroring);
        wizardStatsObject.SetAvoidability(DefenceStatsData.Avoidability);
        wizardStatsObject.SetMaxMana(ManaStatsData.MaxMana);
        wizardStatsObject.SetStartMana(ManaStatsData.StartMana);
        wizardStatsObject.SetManaRegeneration(ManaStatsData.ManaRegeneration);
        wizardStatsObject.SetPassiveManaRegeneration(ManaStatsData.PassiveManaRegeneration);
    }

    public void EquipItem(InventoryItem equipedItem) {

        UpdateAttackStatsData(equipedItem.GetAttackStatsData(), 1);
        UpdateDefenceStatsData(equipedItem.GetDefenceStatsData(), 1);
        UpdateManaStatsData(equipedItem.GetManaStatsData(), 1);
    }

    public void RemoveItem(InventoryItem removedItem) {
        UpdateAttackStatsData(removedItem.GetAttackStatsData(), -1);
        UpdateDefenceStatsData(removedItem.GetDefenceStatsData(), -1);
        UpdateManaStatsData(removedItem.GetManaStatsData(), -1);
    }

    private void UpdateAttackStatsData(AttackStatsData equipedItem, int factor){
        AttackStatsData.CriticalDmg += (equipedItem.CriticalDmg*factor);
        AttackStatsData.CriticalRate += (equipedItem.CriticalRate*factor);
        AttackStatsData.MinBaseDamage += (equipedItem.MinBaseDamage*factor);
        AttackStatsData.MaxBaseDamage += (equipedItem.MaxBaseDamage*factor);
        AttackStatsData.ArmorPenetration += (equipedItem.ArmorPenetration*factor);
    }
    private void UpdateDefenceStatsData(DefenceStatsData equipedItem, int factor){
        DefenceStatsData.MaxHP += (equipedItem.MaxHP*factor);
        DefenceStatsData.Recovery += (equipedItem.Recovery*factor);
        DefenceStatsData.Mirroring += (equipedItem.Mirroring*factor);
        DefenceStatsData.Avoidability += (equipedItem.Avoidability*factor);
    }
    private void UpdateManaStatsData(ManaStatsData equipedItem, int factor){
        ManaStatsData.MaxMana += (equipedItem.MaxMana*factor);
        ManaStatsData.StartMana += (equipedItem.StartMana*factor);
        ManaStatsData.ManaRegeneration += (equipedItem.ManaRegeneration*factor);
        ManaStatsData.PassiveManaRegeneration += (equipedItem.PassiveManaRegeneration*factor);
    }
}

[Serializable]
public class AttackStatsData
{ 
    public int MinBaseDamage = 1;
    public int MaxBaseDamage = 3;
    public float CriticalRate = 0;
    public float CriticalDmg = 0;
    public float ArmorPenetration = 0;
}

[Serializable]
public class DefenceStatsData
{ 
    public int MaxHP = 50;
    public int Recovery = 0;
    public float Avoidability = 0;
    public float Mirroring = 0;
}

[Serializable]
public class ManaStatsData
{ 
    public int MaxMana = 30;
    public float StartMana = 0;
    public int ManaRegeneration = 5;
    public int PassiveManaRegeneration = 0;
}

[Serializable]
public class AttackMagicStatsData
{
    public string name;
    public int multiple;
}

[Serializable]
public class StaffStatsData
{
    public List<string> materials;
    public AttackMagicStatsData SoftMagicStats;
    public AttackMagicStatsData ModerateMagicStats;
    public AttackMagicStatsData HardMagicStats;
    public Material[] GetMaterials()
    {
        return new Material[] {
            Resources.Load<Material>("Materials/" + materials[0]),
        };
    }

}

[Serializable]
public class DefenceMagicStatsData
{
    public string name;
}

[Serializable]
public class CapeStatsData
{
    public List<string> materials;
    public DefenceMagicStatsData SoftMagicStats;
    public DefenceMagicStatsData ModerateMagicStats;
    public DefenceMagicStatsData HardMagicStats;
    public Material[] GetMaterials()
    {
        return new Material[] {
            Resources.Load<Material>("Materials/" + materials[0]),
        };
    }
}

[Serializable]
public class ManaMagicStatsData
{
    public string name;
}

[Serializable]
public class OrbStatsData
{
    public List<string> materials;
    public ManaMagicStatsData SoftMagicStats;
    public ManaMagicStatsData ModerateMagicStats;
    public ManaMagicStatsData HardMagicStats;
    public Material[] GetMaterials()
    {
        return new Material[] {
            Resources.Load<Material>("Materials/" + materials[0]),
        };
    }
}