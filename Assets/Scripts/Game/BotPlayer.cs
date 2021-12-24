using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BotPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player _opponent;
    public DecisionManager.Option GetBotDecision()
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
        if (_opponent.GetManaBar() > 0)
        {
            return decisionWithShoot[rnd.Next(0, 3)];
        }
        else
        {
            return decisionWithoutShoot[rnd.Next(0, 2)];
        }
    }

}
