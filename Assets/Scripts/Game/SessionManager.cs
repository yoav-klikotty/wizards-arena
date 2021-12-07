using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameObject DecisionManagerPrefab;
    DecisionManager DecisionManager;
    [SerializeField] GameObject resultResolverPrefab;
    ResultResolver resultResolver;
    bool isSessionLock = false;
    public DecisionManager.Option opponentOption = DecisionManager.Option.None;
    public DecisionManager.Option playerOption = DecisionManager.Option.None;
    [SerializeField] bool isOnline;
    [SerializeField] Player opponent;
    [SerializeField] Player player;
    public enum GameResult
    {
        Lose,
        Win
    }

    void Start()
    {
        DecisionManager = Instantiate(DecisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
    }

    void Update()
    {

        if (IsBothSidesTookDecision())
        {
            isSessionLock = true;
            RevealDecisions();
        }
        if (SessionEnd())
        {
            StartCoroutine("HandleSessionEndEvent");
        }
    }


    bool IsBothSidesTookDecision()
    {
        if (opponentOption != DecisionManager.Option.None
            &&
            playerOption != DecisionManager.Option.None
            &&
            !isSessionLock)
        {
            return true;
        }
        return false;
    }

    void RevealDecisions()
    {
        Destroy(DecisionManager.gameObject);
        if (resultResolver == null)
        {
            resultResolver = Instantiate(resultResolverPrefab, transform.root).GetComponent<ResultResolver>();
        }
        RenderDecisions();
    }

    void RenderDecisions()
    {
        resultResolver.RenderDecisions(playerOption, opponentOption);
    }

    public void ResetSession()
    {
        playerOption = DecisionManager.Option.None;
        opponentOption = DecisionManager.Option.None;
        Destroy(resultResolver.gameObject);
        if (DecisionManager == null)
        {
            DecisionManager = Instantiate(DecisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
        }
        isSessionLock = false;
    }

    bool SessionEnd()
    {
        if (player.GetHealthBar() == 0)
        {
            LocalStorage.SetLastSessionResult(GameResult.Lose);
            return true;
        }
        else if (opponent.GetHealthBar() == 0)
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
