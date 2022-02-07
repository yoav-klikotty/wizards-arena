using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Magic SoftMagic;
    public Magic ModerateMagic;
    public Magic HardMagic;
    [SerializeField] SkinnedMeshRenderer itemMeshRenderer;
    [SerializeField] ItemType _itemType;
    public void SetMaterials(Material[] mts)
    {
        itemMeshRenderer.materials = mts;
    }
    public void SetMagics(string softMagicPrefName, string moderateMagicPrefName, string hardMagicPrefName)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var softMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + _itemType.ToString() + "/" + softMagicPrefName), transform.position, Quaternion.identity);
        softMagic.transform.parent = gameObject.transform;
        SoftMagic = softMagic.GetComponent<Magic>();
        var moderateMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + _itemType.ToString() + "/" + moderateMagicPrefName), transform.position, Quaternion.identity);
        moderateMagic.transform.parent = gameObject.transform;
        ModerateMagic = moderateMagic.GetComponent<Magic>();
        var hardMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + _itemType.ToString() + "/" + hardMagicPrefName), transform.position, Quaternion.identity);
        hardMagic.transform.parent = gameObject.transform;
        HardMagic = hardMagic.GetComponent<Magic>();
    }
}
