using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] List<GameObject> _options;
    [SerializeField] int initialIndex;

    void Start()
    {
        SelectOptionAnimate(initialIndex);
    }

    public void SelectOptionAnimate(int index)
    {
        for (int i = 0; i < _options.Count; i++)
        {
            if (i == index)
            {
                LeanTween.scale(_options[i], new Vector3(2, 2, 2), 0.3f);
            }
            else {
                LeanTween.scale(_options[i], new Vector3(1, 1, 1), 0.3f);
            }
        }
    }

}
