using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameObject _languages;
    [SerializeField] Toggle _musicCheckmark;
    [SerializeField] Toggle _sfxCheckmark;

    void OnEnable()
    {
        if(SoundManager.Instance.isMusicOn)
        {
            _musicCheckmark.SetIsOnWithoutNotify(true);
        }
        else
        {
            _musicCheckmark.SetIsOnWithoutNotify(false);
        }

        if(SoundManager.Instance.isSfxOn)
        {
            _sfxCheckmark.SetIsOnWithoutNotify(true);
        }
        else
        {
            _sfxCheckmark.SetIsOnWithoutNotify(false);
        }
    }
    
    public void OpenLanguage()
    {
        _languages.SetActive(true);
    }
    public void OpenGraphics()
    {
        _languages.SetActive(false);
    }
    public void ResetGame()
    {
        WizardStatsController.Instance.ResetData();
        SceneManager.LoadScene("Dashboard");
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }

    public void ToggleMusic()
    {
        SoundManager.Instance.isMusicOn = !SoundManager.Instance.isMusicOn;
        if(SoundManager.Instance.isMusicOn)
        {
            SoundManager.Instance.PlayGameThemeSound();
        }
        else{
            SoundManager.Instance.StopGameThemeSound();
        }
    }

    public void ToggleSfx()
    {
        SoundManager.Instance.isSfxOn = !SoundManager.Instance.isSfxOn;
    }
}
