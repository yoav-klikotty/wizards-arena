using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : ItemData
{
    void Awake() {
        Name = "Staff";
        RequiredLevel = 1;
        Description = "Staff";
        Type = "Staff";
        NumOfSockets = 0;
        Attributes.MaxDmg = 5;
        Attributes.MinDmg = 10;
    }
}
