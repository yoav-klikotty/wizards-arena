using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedStaff : ItemData
{
    void Awake() {
        Name = "Advanced Staff";
        RequiredLevel = 5;
        Description = "Weak staff";
        Type = "Staff";
        NumOfSockets = 0;
        Attributes.MaxDmg = 7;
        Attributes.MinDmg = 15;
    }
}
