using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool IsSFXOn = true;
    public int ActivePlayers = 0;
    public int NumOfDeathmatchPlayers = 2;
    public int TimeToPlay = 3;
    public int EnergyCost = 1;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(SetInitialLanguage());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetSFX()
    {
        this.IsSFXOn = !this.IsSFXOn;
    }
    public IEnumerator SetInitialLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;
        int languageIndex = 0;
        switch(Application.systemLanguage){
            case SystemLanguage.English:
                languageIndex = 0;
                break;
            default:
                languageIndex = 0;
                break;
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }


}
