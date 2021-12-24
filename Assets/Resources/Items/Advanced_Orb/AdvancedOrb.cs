using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedOrb : ItemData
{
    void Awake() {
        Name = "Advanced Orb";
        RequiredLevel = 5;
        Description = "Weak orb";
        Type = "Orb";
        NumOfSockets = 0;
        Attributes.MaxMana = 10;
    }
}
