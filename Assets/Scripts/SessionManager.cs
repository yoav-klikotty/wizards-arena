using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] DecitionManager decitionManager;
    [SerializeField] ResultResolver resultResolver;
    bool isSessionLock = false;
    DecitionManager.Option masterOption = DecitionManager.Option.Random;
    DecitionManager.Option slaveOption = DecitionManager.Option.Random;
    [SerializeField] bool isOnline;
    [SerializeField] MasterStatus masterStatus;
    [SerializeField] SlaveStatus slaveStatus;

    PhotonView photonView;

    public enum GameResult
    {
        Lose,
        Win
    }
    void Start()
    {
        photonView = PhotonView.Get(this);
        if (PhotonNetwork.IsMasterClient)
        {
            masterStatus.SetBackgroud();
        }
        else
        {
            slaveStatus.SetBackgroud();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnline)
        {
            HandleUpdateForOnlineGame();
        }
        else
        {
            HandleUpdateForBotGame();
        }
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
        if (masterOption != DecitionManager.Option.Random
            &&
            slaveOption != DecitionManager.Option.Random
            &&
            !isSessionLock)
        {
            return true;
        }
        return false;
    }

    void HandleUpdateForOnlineGame()
    {
        if (decitionManager.IsDecitionMakingOver())
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("UpdateMasterDecition", RpcTarget.All, decitionManager.GetOption());
            }
            else
            {
                photonView.RPC("UpdateSlaveDecition", RpcTarget.All, decitionManager.GetOption());
            }

        }
        
    }

    void HandleUpdateForBotGame()
    {
        if (decitionManager.IsDecitionMakingOver())
        {
            UpdateMasterDecition(decitionManager.GetOption());
            UpdateSlaveDecition(DecitionManager.Option.Ammo);
        }
    }

    [PunRPC]
    void UpdateMasterDecition(DecitionManager.Option option)
    {
        masterOption = option;
    }

    [PunRPC]
    void UpdateSlaveDecition(DecitionManager.Option option)
    {
        slaveOption = option;
    }
    void RevealDecitions()
    {
        decitionManager.ResetDecitionManager();
        decitionManager.gameObject.SetActive(false);
        resultResolver.gameObject.SetActive(true);
        RenderDecitions();
        HandleResults();
    }
    void RenderDecitions()
    {
        resultResolver.RenderDecitions(masterOption, slaveOption);
    }

    void HandleResults()
    {
        if (masterOption == DecitionManager.Option.Ammo && slaveOption == DecitionManager.Option.Ammo)
        {
            masterStatus.IncreaseAmmo();
            slaveStatus.IncreaseAmmo();
        }
        else if (masterOption == DecitionManager.Option.Ammo && slaveOption == DecitionManager.Option.Shield)
        {
            masterStatus.IncreaseAmmo();
        }
        else if (masterOption == DecitionManager.Option.Ammo && slaveOption == DecitionManager.Option.Shoot)
        {
            masterStatus.IncreaseAmmo();
            masterStatus.ReduceHealthBar();
            slaveStatus.ReduceAmmo();
        }
        else if (masterOption == DecitionManager.Option.Shield)
        {
            if (slaveOption == DecitionManager.Option.Ammo)
            {
                slaveStatus.IncreaseAmmo();
            }
            else if (slaveOption == DecitionManager.Option.Shoot)
            {
                slaveStatus.ReduceAmmo();
            }
        }
        else if (masterOption == DecitionManager.Option.Shoot && slaveOption == DecitionManager.Option.Shield)
        {
            masterStatus.ReduceAmmo();
        }
        else if (masterOption == DecitionManager.Option.Shoot && slaveOption == DecitionManager.Option.Ammo)
        {
            masterStatus.ReduceAmmo();
            slaveStatus.ReduceHealthBar();
            slaveStatus.IncreaseAmmo();
        }
        else if (masterOption == DecitionManager.Option.Shoot && slaveOption == DecitionManager.Option.Shoot)
        {
            masterStatus.ReduceAmmo();
            slaveStatus.ReduceAmmo();
            slaveStatus.ReduceHealthBar();
            masterStatus.ReduceHealthBar();
        }

    }
    void ResetSession()
    {
        masterOption = DecitionManager.Option.Random;
        slaveOption = DecitionManager.Option.Random;
        isSessionLock = false;
        decitionManager.gameObject.SetActive(true);
        resultResolver.gameObject.SetActive(false);
    }
    bool SessionEnd()
    {
        if (masterStatus.GetHealthBar() == 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PlayerPrefs.SetInt("LastResult", (int)GameResult.Lose);
            }
            else
            {
                PlayerPrefs.SetInt("LastResult", (int)GameResult.Win);
            }
            return true;
        }
        else if (slaveStatus.GetHealthBar() == 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PlayerPrefs.SetInt("LastResult", (int)GameResult.Win);
            }
            else
            {
                PlayerPrefs.SetInt("LastResult", (int)GameResult.Lose);
            }
            return true;
        }
        return false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("disconnented");
    }
}
