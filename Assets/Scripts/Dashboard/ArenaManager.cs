using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    private PlayerStatsController _playerStatsController = new PlayerStatsController();
    [SerializeField] NavigationPanel _navigationPanel;
    [SerializeField] GameObject GameModes;
    PlayerStatsData playerStatsData;


    void Start()
    {
        SoundManager.Instance.PlayGameThemeSound();
    }

    public void OpenGameModes()
    {
        SoundManager.Instance.PlayButtonSound();
        playerStatsData = _playerStatsController.GetPlayerStatsData();
        if (playerStatsData.GetCrystals() > 0)
        {
            GameModes.SetActive(true);
        }
        else
        {
            _navigationPanel.PageChange(3);
        }
    }
    public void ExitGameModes()
    {
        SoundManager.Instance.PlayButtonSound();
        GameModes.SetActive(false);
    }
    public void Start2V2Game()
    {
        SoundManager.Instance.PlayButtonSound();
        StartDeathmatchGame(2);
    }
    public void Start3V3Game()
    {
        SoundManager.Instance.PlayButtonSound();
        StartDeathmatchGame(3);
    }
    public void Start4V4Game()
    {
        SoundManager.Instance.PlayButtonSound();
        StartDeathmatchGame(4);
    }

    public void StartDeathmatchGame(int numberOfPlayers)
    {
        GameManager.Instance.NumOfDeathmatchPlayers = numberOfPlayers;
        StartCoroutine(SoundManager.Instance.FadeOutGameThemeSong(1f));
        playerStatsData.SetCrystals(playerStatsData.GetCrystals() - 1);
        _playerStatsController.SavePlayerStatsData(playerStatsData, true);
        SceneManager.LoadScene("Search");
    }
    public void LoadMasteriesScene()
    {
        SceneManager.LoadScene("Masteries");
    }
    public void LoadMagicsScene()
    {
        SceneManager.LoadScene("Magics");
    }

}
