using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _masteryAttributes;

    public void Active(bool isActive)
    {
        if (isActive)
        {
            var tempColor = _icon.color;
            tempColor.a = 1f;
            _icon.color = tempColor;
            tempColor = _masteryAttributes.color;
            tempColor.a = 1f;
            _masteryAttributes.color = tempColor;
        }
        else
        {
            var tempColor = _icon.color;
            tempColor.a = 0.3f;
            _icon.color = tempColor;
            tempColor = _masteryAttributes.color;
            tempColor.a = 0.3f;
            _masteryAttributes.color = tempColor;
        }
    }

}
