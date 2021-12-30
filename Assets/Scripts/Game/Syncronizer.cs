using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Syncronizer : MonoBehaviour
{
    SessionManager _sessionManager;
    PhotonView _photonView;
    [SerializeField] BotPlayer _botPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        _photonView = PhotonView.Get(this);
        if (PhotonNetwork.IsConnected)
        {
            WizardStatsData wizardStatsData = new WizardStatsData();
            _photonView.RPC("SyncOpponentPlayer", RpcTarget.Others, JsonUtility.ToJson(wizardStatsData));
        }
        else
        {
        }
    }

    public void UpdatePlayersDecision(DecisionManager.Option option)
    {
        _sessionManager.PlayerOption = option;
        if (PhotonNetwork.IsConnected)
        {
            _photonView.RPC("SyncOpponentDecision", RpcTarget.Others, option);
        }
        else
        {
            _sessionManager.OpponentOption = _botPlayer.GetBotDecision();
            Debug.Log(_sessionManager.OpponentOption);
        }
    }

    [PunRPC]
    void SyncOpponentDecision(DecisionManager.Option option)
    {
        _sessionManager.OpponentOption = option;
    }

    [PunRPC]
    void SyncOpponentPlayer(string wizardStatsDataRaw)
    {
        WizardStatsData wizardStatsData = JsonUtility.FromJson<WizardStatsData>(wizardStatsDataRaw);
        _sessionManager.OpponentWizardStatsData = wizardStatsData;
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
}
