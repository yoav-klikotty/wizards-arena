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
        photonView.RPC("SyncOpponentDecision", RpcTarget.Others, option);
    }

    [PunRPC]
    void SyncOpponentDecision(DecisionManager.Option option)
    {
        sessionManager.opponentOption = option;
    }

    [PunRPC]
    void SyncOpponentPlayer(Player player)
    {
        opponent.SetAmmo(player.GetAmmo());
        opponent.SetLife(player.GetLife());
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.LogError("disconnented");
    //}
}
