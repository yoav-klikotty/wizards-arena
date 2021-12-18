using System;

[Serializable]
public class WizardStatsData
{
    public AttackStatsData AttackStatsData;
    public DefenceStatsData DefenceStatsData;
    public ManaStatsData ManaStatsData;
    public string WizardPrefabName = "";
    public string CapePrefabName = "";
    public string WandPrefabName = "";
    public string OrbPrefabName = "";

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
