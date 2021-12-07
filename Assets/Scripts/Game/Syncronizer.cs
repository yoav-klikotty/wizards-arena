using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Syncronizer : MonoBehaviour
{
    SessionManager sessionManager;
    PhotonView photonView;
    [SerializeField] Player opponent;
    [SerializeField] Player player;
    [SerializeField] BotPlayer botPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        photonView = PhotonView.Get(this);
        // photonView.RPC("SyncOpponentPlayer", RpcTarget.Others, player);
    }

    public void UpdatePlayersDecision(DecisionManager.Option option)
    {
        Debug.Log("HandleUpdateForOnlineGame " + option);
        sessionManager.playerOption = option;
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("SyncOpponentDecision", RpcTarget.Others, option);
        }
        else 
        {
            botPlayer.GetBotDecision();
        }
    }

    [PunRPC]
    void SyncOpponentDecision(DecisionManager.Option option)
    {
        sessionManager.opponentOption = option;
    }

    [PunRPC]
    void SyncOpponentPlayer(Player player)
    {

    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
}
