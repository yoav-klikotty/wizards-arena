using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameObject _decisionManagerPrefab;
    DecisionManager _decisionManager;
    [SerializeField] GameObject _resultResolverPrefab;
    ResultResolver _resultResolver;
    bool _isSessionLock = false;
    bool _isSessionEndLock = false;
    public DecisionManager.Option OpponentOption = DecisionManager.Option.None;
    public WizardStatsData OpponentWizardStatsData;
    public DecisionManager.Option PlayerOption = DecisionManager.Option.None;
    public WizardStatsData PlayerWizardStatsData;
    [SerializeField] bool _isOnline;
    [SerializeField] Player _opponent;
    [SerializeField] Player _player;
    public enum GameResult
    {
        Lose,
        Win
    }

    void Start()
    {
        _decisionManager = Instantiate(_decisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
    }

    void Update()
    {

        if (IsBothSidesTookDecision())
        {
            _isSessionLock = true;
            RevealDecisions();
        }
        if (SessionEnd() && !_isSessionEndLock)
        {
            _isSessionLock = true;
            StartCoroutine("HandleSessionEndEvent");
        }
    }


    bool IsBothSidesTookDecision()
    {
        if (OpponentOption != DecisionManager.Option.None
            &&
            PlayerOption != DecisionManager.Option.None
            &&
            !_isSessionLock)
        {
            return true;
        }
        return false;
    }

    void RevealDecisions()
    {
        Destroy(_decisionManager.gameObject);
        if (_resultResolver == null)
        {
            _resultResolver = Instantiate(_resultResolverPrefab, transform.root).GetComponent<ResultResolver>();
        }
        RenderDecisions();
    }

    void RenderDecisions()
    {
        _resultResolver.RenderDecisions(PlayerOption, OpponentOption);
    }

    public void ResetSession()
    {
        PlayerOption = DecisionManager.Option.None;
        OpponentOption = DecisionManager.Option.None;
        Destroy(_resultResolver.gameObject);
        if (_decisionManager == null)
        {
            _decisionManager = Instantiate(_decisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
        }
        _isSessionLock = false;
    }

    bool SessionEnd()
    {
        if (_player.GetHealthBar() == 0)
        {
            LocalStorage.SetLastSessionResult(GameResult.Lose);
            return true;
        }
        else if (_opponent.GetHealthBar() == 0)
        {
            LocalStorage.SetLastSessionResult(GameResult.Win);
            return true;
        }
        return false;
    }
    IEnumerator HandleSessionEndEvent()
    {
		yield return new WaitForSeconds(4);
        SceneManager.LoadScene("EndGame");
		
    }

}
