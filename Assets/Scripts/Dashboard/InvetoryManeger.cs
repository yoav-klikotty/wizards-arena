using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManeger : MonoBehaviour
{
    [SerializeField] string[] _items;
    [SerializeField] FilterPanel _panelFilter;
    [SerializeField] InventoryItem[] _prefabs;

    void Start()
    {
        var slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        for(int i = 0; i < _items.Length; i++){
            var item = Instantiate(_prefabs[i%_prefabs.Length], slots[i].transform.position, slots[i].transform.rotation, slots[i].transform);
        }
    }
}
