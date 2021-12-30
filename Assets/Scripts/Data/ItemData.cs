using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ItemData : MonoBehaviour
{
    public string Name;
    public int RequiredLevel;
    public string Description;
    public ItemAttributes Attributes = new ItemAttributes();
    public string Type;
    public int NumOfSockets;
    private List<string> _attributesList = new List<string>();
    public List<string> GetAttributes(){
        foreach(var property in Attributes.GetType().GetFields()) {
            string value = property.GetValue(Attributes).ToString();
            if(value != "0") {
                _attributesList.Add(property.Name + ": " + value);
            }
        }
        return _attributesList;
    }
}
