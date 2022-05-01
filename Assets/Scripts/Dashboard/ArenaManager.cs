using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    [SerializeField] NavigationPanel _navigationPanel;
    PlayerStatsData playerStatsData;
    [SerializeField] GameObject _settings;
    [SerializeField] TMP_Text _mmr;

    void Start()
    {
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        SoundManager.Instance.PlayGameThemeSound();
    }

    public void OpenOnlineMenu()
    {
        SoundManager.Instance.PlayButtonSound();
        if (playerStatsData.GetCrystals() > 0)
        {
            StartCoroutine(SoundManager.Instance.FadeOutGameThemeSong(1f));
            SceneManager.LoadScene("OnlineMenu");
        }
        else
        {
            _navigationPanel.PageChange(3);
        }
    }
    public void LoadMasteriesScene()
    {
        SoundManager.Instance.PlayButtonSound();
        LocalStorage.SetDashboardPage(1);
        SceneManager.LoadScene("Masteries");
    }
    public void LoadMagicsScene()
    {
        SoundManager.Instance.PlayButtonSound();
        LocalStorage.SetDashboardPage(1);
        SceneManager.LoadScene("Magics");
    }
    public void OpenSettings()
    {
        _settings.SetActive(true);
    }

}
