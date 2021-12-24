using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cape : ItemData
{
    void Awake() {
        Name = "Cape";
        RequiredLevel = 1;
        Description = "Cape";
        Type = "Cape";
        NumOfSockets = 0;
        Attributes.MaxHP = 10;
    }
}
