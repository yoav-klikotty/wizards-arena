using UnityEngine;
using UnityEngine.UI;

public class ResultResolver : MonoBehaviour
{
    SessionManager sessionManager;
    public Image OpponentChoice;
    public Image PlayerChoice;
    public Sprite Shield;
    public Sprite Ammo;
    Inventory opponentInventory;
    Inventory playerInventory;
    void Awake()
    {
        sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        playerInventory = GameObject.Find("PlayerHUD").transform.Find("Inventory").GetComponent<Inventory>();
        opponentInventory = GameObject.Find("OpponentHUD").transform.Find("Inventory").GetComponent<Inventory>();

    }
    public void RenderDecisions(DecisionManager.Option playerDecision, DecisionManager.Option opponentDecision){
        RenderDecision(opponentDecision, OpponentChoice, opponentInventory);
        RenderDecision(playerDecision, PlayerChoice, playerInventory);
        InvokeRepeating("FinishResolving", 1, 0);
    }

    void RenderDecision(DecisionManager.Option Decision, Image image, Inventory Inventory){
        if (Decision == DecisionManager.Option.Reload){
            image.sprite = Ammo;
        }
        else if (Decision == DecisionManager.Option.Protect){
            image.sprite = Shield;
        }
        else {
            image.sprite = Inventory.GetCurrentWeapon().sprite;
        }
    }
    void FinishResolving(){
        sessionManager.ResetSession();
    }
}
