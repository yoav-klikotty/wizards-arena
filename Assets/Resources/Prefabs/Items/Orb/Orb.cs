using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : ItemData
{
    void Awake() {
        Name = "Orb";
        RequiredLevel = 1;
        Description = "Orb";
        Type = "Orb";
        NumOfSockets = 0;
        Attributes.MaxMana = 5;
    }
}
