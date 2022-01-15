using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Syncronizer : MonoBehaviour
{
    SessionManager _sessionManager;
    PhotonView _photonView;
    [SerializeField] BotPlayer _botPlayer;
    [SerializeField] Player _opponent;
    WizardStatsController _wizardStatsController = new WizardStatsController();


    // Start is called before the first frame update
    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        _photonView = PhotonView.Get(this);
        if (PhotonNetwork.IsConnected)
        {
            WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
            _photonView.RPC("SyncOpponentPlayer", RpcTarget.Others, JsonUtility.ToJson(wizardStatsData));
        }
        else
        {
            WizardStatsData wizardStatsData = _wizardStatsController.GetWizardStatsData();
            _opponent.UpdateWizard(wizardStatsData);
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
        _opponent.UpdateWizard(wizardStatsData);
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
}
