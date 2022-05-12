using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class EndSession : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text Result;
    [SerializeField] TMP_Text ClaimButtonText;
    SessionManager.GameResult result;
    [SerializeField] SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] Material _victoryMaterial;
    [SerializeField] Material _defeatMaterial;
    [SerializeField] Animation _starsAnim;
    [SerializeField] bool _debug;
    [SerializeField] bool _isWon;
    [SerializeField] GameObject PrizeContainer;

    private PlayerStatsController _playerStatsController = new PlayerStatsController();

    void Start()
    {
        result = LocalStorage.GetLastSessionResult();
        if (!_debug)
        {
            _isWon = result == SessionManager.GameResult.First;
        }
        RenderScore();
        InvokeRepeating("Disconnect", 1, 0);
    }

    public void Collect()
    {
        PhotonNetwork.Disconnect();
    }

    void RenderScore()
    {
        RenderTextScore();
        RenderSoundScore();
        RenderWizardScore();
        StartStarsAnim();
        RenderPrizes();

    }

    void RenderPrizes()
    {
        if (!_isWon)
        {
            PrizeContainer.SetActive(false);
        }
        else
        {
            PrizeContainer.SetActive(true);
        }
    }

    void RenderTextScore()
    {
        if (!_isWon)
        {
            ClaimButtonText.text = "Retry";
        }
        else
        {
            ClaimButtonText.text = "Claim";
        }
        Result.text = result.ToString();
    }
    void RenderSoundScore()
    {
        if (!_isWon)
        {
            SoundManager.Instance.PlayYouLoseSound();
        }
        else
        {
            SoundManager.Instance.PlayYouWinSound();
        }
    }
    void RenderWizardScore()
    {
        Material materialToAdd;
        if (!_isWon)
        {
            materialToAdd = _defeatMaterial;
        }
        else
        {
            materialToAdd = _victoryMaterial;
        }
        Material[] mts = 
        {
            materialToAdd
        };
        _skinnedMeshRenderer.materials = mts;

    }

    void StartStarsAnim()
    {
        if (_isWon)
        {
            _starsAnim.Play();
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        if (_isWon)
        {
            PlayerStatsData playerStatsData = _playerStatsController.GetPlayerStatsData();
            playerStatsData.SetCoins(playerStatsData.GetCoins() + 100);
            playerStatsData.AddXP(10);
            _playerStatsController.SavePlayerStatsData(playerStatsData, false);
        }
        SceneManager.LoadScene("Dashboard");
    }

    
}
