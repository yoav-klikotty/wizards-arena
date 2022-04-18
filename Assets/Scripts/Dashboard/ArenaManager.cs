using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    [SerializeField] NavigationPanel _navigationPanel;
    PlayerStatsData playerStatsData;


    void Start()
    {
        SoundManager.Instance.PlayGameThemeSound();
    }

    public void OpenOnlineMenu()
    {
        SoundManager.Instance.PlayButtonSound();
        playerStatsData = _playerStatsController.GetPlayerStatsData();
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
        LocalStorage.SetDashboardPage(1);
        SceneManager.LoadScene("Masteries");
    }
    public void LoadMagicsScene()
    {
        LocalStorage.SetDashboardPage(1);
        SceneManager.LoadScene("Magics");
    }

}
