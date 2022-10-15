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
    [SerializeField] Animation _starsAnim;
    [SerializeField] bool _debug;
    [SerializeField] bool _isWon;
    [SerializeField] TMP_Text MMRAmount;
    [SerializeField] TMP_Text XPAmount;
    [SerializeField] TMP_Text CoinAmount;
    private int rankDiff = 0;

    void Start()
    {
        if (!_debug)
        {
            _isWon = result == SessionManager.GameResult.First;
        }
        RenderScore();
        InvokeRepeating("Disconnect", 1, 0);
    }

    public void UpdateWizardRank(int numOfplayers, SessionManager.GameResult myPlace, int rankDelta)
    {
        var playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
        switch (myPlace)
        {
            case SessionManager.GameResult.First:
                rankDiff = rankDelta;
                break;
            case SessionManager.GameResult.Second:
                switch (numOfplayers)
                {
                    case 2:
                        rankDiff = rankDelta * -1;
                        break;
                    case 3:
                        break;
                    case 4:
                        rankDiff = rankDelta / 2;
                        break;
                }
                break;
            case SessionManager.GameResult.Third:
                switch (numOfplayers)
                {
                    case 3:
                        rankDiff = rankDelta * -1;
                        break;
                    case 4:
                        rankDiff = (rankDelta * -1) / 2;
                        break;
                }
                break;
            case SessionManager.GameResult.Fourth:
                rankDiff = rankDelta * -1;
                break;
        }
        playerStatsData.RankStatsData.AddRank(rankDiff);
        PlayerStatsController.Instance.SavePlayerStatsData(playerStatsData);
    }

    public void Collect()
    {
        PhotonNetwork.Disconnect();
    }

    public void SetResult(SessionManager.GameResult result)
    {
        this.result = result;
    }

    void RenderScore()
    {
        RenderTextScore();
        RenderSoundScore();
        StartStarsAnim();
        RenderPrizes();

    }

    void RenderPrizes()
    {
        MMRAmount.text = rankDiff + "";
        if (!_isWon)
        {
            XPAmount.text = 0 + "";
            CoinAmount.text = 0 + "";
        }
        else
        {
            XPAmount.text = 10 + "";
            CoinAmount.text = 100 + "";
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
            PlayerStatsData playerStatsData = PlayerStatsController.Instance.GetPlayerStatsData();
            // playerStatsData.SetCoins(playerStatsData.GetCoins() + 100);
            // playerStatsData.AddXP(10);
            PlayerStatsController.Instance.SavePlayerStatsData(playerStatsData);
        }
        SceneManager.LoadScene("Dashboard");
    }


}
