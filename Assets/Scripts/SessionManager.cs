using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject DecisionManagerPrefab;
    DecisionManager DecisionManager;
    [SerializeField] GameObject resultResolverPrefab;
    ResultResolver resultResolver;
    bool isSessionLock = false;
    public DecisionManager.Option opponentOption = DecisionManager.Option.None;
    public DecisionManager.Option playerOption = DecisionManager.Option.None;
    [SerializeField] bool isOnline;
    [SerializeField] Player opponent;
    [SerializeField] Inventory opponentInventory;
    [SerializeField] Player player;
    [SerializeField] Inventory playerInventory;
    public enum GameResult
    {
        Lose,
        Win
    }

    void Start()
    {
        DecisionManager = Instantiate(DecisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsBothSidesTookDecision())
        {
            isSessionLock = true;
            RevealDecisions();
        }
        if (SessionEnd())
        {
            SceneManager.LoadScene("EndGame");
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
        HandleResults();
    }

    void RenderDecisions()
    {
        resultResolver.RenderDecisions(playerOption, opponentOption);
    }

    void HandleResults()
    {
        if (playerOption == DecisionManager.Option.Reload && opponentOption == DecisionManager.Option.Reload)
        {
            player.IncreaseAmmo();
            opponent.IncreaseAmmo();
        }
        else if (playerOption == DecisionManager.Option.Reload && opponentOption == DecisionManager.Option.Protect)
        {
            player.IncreaseAmmo();
        }
        else if (playerOption == DecisionManager.Option.Reload && opponentOption == DecisionManager.Option.Shoot)
        {
            player.IncreaseAmmo();
            player.ReduceHealthBar(opponentInventory.GetCurrentWeapon().damage);
            opponent.ReduceAmmo();
        }
        else if (playerOption == DecisionManager.Option.Protect)
        {
            if (opponentOption == DecisionManager.Option.Reload)
            {
                opponent.IncreaseAmmo();
            }
            else if (opponentOption == DecisionManager.Option.Shoot)
            {
                opponent.ReduceAmmo();
            }
        }
        else if (playerOption == DecisionManager.Option.Shoot && opponentOption == DecisionManager.Option.Protect)
        {
            player.ReduceAmmo();
        }
        else if (playerOption == DecisionManager.Option.Shoot && opponentOption == DecisionManager.Option.Reload)
        {
            player.ReduceAmmo();
            opponent.ReduceHealthBar(playerInventory.GetCurrentWeapon().damage);
            opponent.IncreaseAmmo();
        }
        else if (playerOption == DecisionManager.Option.Shoot && opponentOption == DecisionManager.Option.Shoot)
        {
            player.ReduceAmmo();
            opponent.ReduceAmmo();
            opponent.ReduceHealthBar(playerInventory.GetCurrentWeapon().damage);
            player.ReduceHealthBar(opponentInventory.GetCurrentWeapon().damage);
        }

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


}
