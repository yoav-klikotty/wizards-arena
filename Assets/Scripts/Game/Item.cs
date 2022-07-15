using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer itemMeshRenderer;
    [SerializeField] ItemType _itemType;
    public void SetMaterials(Material[] mts)
    {
        itemMeshRenderer.materials = mts;
    }
}
