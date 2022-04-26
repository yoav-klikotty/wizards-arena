using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject _languages;
    
    public void OpenLanguage()
    {
        _languages.SetActive(true);
    }

    public void OpenGraphics()
    {
        _languages.SetActive(false);
    }
}
