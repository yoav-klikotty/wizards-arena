using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BotPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player Opponent;
    [SerializeField] SessionManager SessionManager;
    public void GetBotDecision()
    {
        Random rnd = new Random();
        DecisionManager.Option[] decisionWithoutShoot = { 
            DecisionManager.Option.Protect, 
            DecisionManager.Option.Reload, 
        };
         DecisionManager.Option[] decisionWithShoot = { 
            DecisionManager.Option.Reload, 
            DecisionManager.Option.Protect, 
            DecisionManager.Option.Shoot 
        };
        if (Opponent.GetManaBar() > 0)
        {
            SessionManager.opponentOption = decisionWithShoot[rnd.Next(0,3)];
        }
        else
        {
            SessionManager.opponentOption = decisionWithoutShoot[rnd.Next(0,2)];
        }
    }

}
