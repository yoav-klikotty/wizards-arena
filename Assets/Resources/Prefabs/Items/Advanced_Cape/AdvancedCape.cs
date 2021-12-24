using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class AdvancedCape : ItemData
{
    void Awake() {
        Name = "Advanced Cape";
        RequiredLevel = 5;
        Description = "Weak cape";
        Type = "Cape";
        NumOfSockets = 0;
        Attributes.MaxHP = 20;
    }
}
