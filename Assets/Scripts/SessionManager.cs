using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] GameObject decitionManagerPrefab;
    DecitionManager decitionManager;
    [SerializeField] ResultResolver resultResolver;
    bool isSessionLock = false;
    public DecitionManager.Option opponentOption = DecitionManager.Option.None;
    public DecitionManager.Option playerOption = DecitionManager.Option.None;
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
        decitionManager = Instantiate(decitionManagerPrefab, transform.root).GetComponent<DecitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsBothSidesTookDecition())
        {
            isSessionLock = true;
            resultResolver.ResetResultResolver();
            RevealDecitions();
        }
        if (resultResolver.isResultResolverDone)
        {
            ResetSession();
        }
        if (SessionEnd())
        {
            SceneManager.LoadScene("EndGame");
        }
    }


    bool IsBothSidesTookDecition()
    {
        if (opponentOption != DecitionManager.Option.None
            &&
            playerOption != DecitionManager.Option.None
            &&
            !isSessionLock)
        {
            return true;
        }
        return false;
    }

    void RevealDecitions()
    {
        Destroy(decitionManager.gameObject);
        resultResolver.gameObject.SetActive(true);
        RenderDecitions();
        HandleResults();
    }

    void RenderDecitions()
    {
        resultResolver.RenderDecitions(playerOption, opponentOption);
    }

    void HandleResults()
    {
        if (playerOption == DecitionManager.Option.Reload && opponentOption == DecitionManager.Option.Reload)
        {
            player.IncreaseAmmo();
            opponent.IncreaseAmmo();
        }
        else if (playerOption == DecitionManager.Option.Reload && opponentOption == DecitionManager.Option.Protect)
        {
            player.IncreaseAmmo();
        }
        else if (playerOption == DecitionManager.Option.Reload && opponentOption == DecitionManager.Option.Shoot)
        {
            player.IncreaseAmmo();
            player.ReduceHealthBar();
            opponent.ReduceAmmo();
        }
        else if (playerOption == DecitionManager.Option.Protect)
        {
            if (opponentOption == DecitionManager.Option.Reload)
            {
                opponent.IncreaseAmmo();
            }
            else if (opponentOption == DecitionManager.Option.Shoot)
            {
                opponent.ReduceAmmo();
            }
        }
        else if (playerOption == DecitionManager.Option.Shoot && opponentOption == DecitionManager.Option.Protect)
        {
            player.ReduceAmmo();
        }
        else if (playerOption == DecitionManager.Option.Shoot && opponentOption == DecitionManager.Option.Reload)
        {
            player.ReduceAmmo();
            opponent.ReduceHealthBar();
            opponent.IncreaseAmmo();
        }
        else if (playerOption == DecitionManager.Option.Shoot && opponentOption == DecitionManager.Option.Shoot)
        {
            player.ReduceAmmo();
            opponent.ReduceAmmo();
            opponent.ReduceHealthBar();
            player.ReduceHealthBar();
        }

    }

    void ResetSession()
    {
        playerOption = DecitionManager.Option.None;
        opponentOption = DecitionManager.Option.None;
        isSessionLock = false;
        if (decitionManager == null)
        {
            decitionManager = Instantiate(decitionManagerPrefab, transform.root).GetComponent<DecitionManager>();
        }
        resultResolver.gameObject.SetActive(false);
    }

    bool SessionEnd()
    {
        if (player.GetHealthBar() == 0)
        {
            PlayerPrefs.SetInt("LastResult", (int)GameResult.Lose);
            return true;
        }
        else if (opponent.GetHealthBar() == 0)
        {
            PlayerPrefs.SetInt("LastResult", (int)GameResult.Win);
            return true;
        }
        return false;
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
}
