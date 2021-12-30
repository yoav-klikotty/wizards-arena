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
}

[Serializable]
public class AttackStatsData
{ 
    public int MinBaseDamage = 0;
    public int MaxBaseDamage = 0;
    public float CriticalRate = 0;
    public float ArmorPenetration = 0;
}

[Serializable]
public class DefenceStatsData
{ 
    public int MaxHP = 0;
    public int Recovery = 0;
    public float Avoidability = 0;
    public float Mirroring = 0;
}

[Serializable]
public class ManaStatsData
{ 
    public int MaxMana = 0;
    public float StartMana = 0;
    public int ManaRegeneration = 0;
    public int PassiveManaRegeneration = 0;
}

[Serializable]
public class AttackMagicStatsData
{
    public string name;
    public int multiple;
    public int requiredMana;
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