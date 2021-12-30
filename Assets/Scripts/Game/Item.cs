using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Magic SoftMagic;
    public Magic ModerateMagic;
    public Magic HardMagic;
    [SerializeField] SkinnedMeshRenderer itemMeshRenderer;

    void Start()
    {
    }

    public void SetMaterials(Material [] mts)
    {
        itemMeshRenderer.materials = mts;
    }
    public void SetSoftMagic(string softMagicPrefName)
    {
        var softMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + softMagicPrefName),transform.position, Quaternion.identity);
        softMagic.transform.parent = gameObject.transform;
        SoftMagic = softMagic.GetComponent<Magic>();
    }
    public void SetModerateMagic(string moderateMagicPrefName)
    {
        var moderateMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + moderateMagicPrefName),transform.position, Quaternion.identity);
        moderateMagic.transform.parent = gameObject.transform;
        ModerateMagic = moderateMagic.GetComponent<Magic>();
    }

    public void SetHardMagic(string hardMagicPrefName)
    {
        var hardMagic = (GameObject)Instantiate(Resources.Load("Prefabs/" + "Magics/" + hardMagicPrefName),transform.position, Quaternion.identity);
        hardMagic.transform.parent = gameObject.transform;
        HardMagic = hardMagic.GetComponent<Magic>();
    }
}
