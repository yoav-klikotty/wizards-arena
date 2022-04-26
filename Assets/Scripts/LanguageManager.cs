using UnityEngine;
using UnityEngine.Localization.Settings;
public class LanguageManager : MonoBehaviour
{
    public void SetLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }
}
