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
    }

    public void HandleUpdateForOnlineGame(DecitionManager.Option option)
    {
        sessionManager.playerOption = option;
        photonView.RPC("SyncDecition", RpcTarget.Others, option);
    }

    [PunRPC]
    void SyncDecition(DecitionManager.Option option)
    {
        sessionManager.opponentOption = option;
    }

}
