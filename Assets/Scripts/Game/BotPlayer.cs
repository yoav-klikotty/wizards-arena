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
            DecisionManager.Option.SoftAttack,
            DecisionManager.Option.ModerateAttack,
            DecisionManager.Option.HardAttack,
        };
        int currentMana = _opponent.GetMana();
        if (currentMana == 0)
        {
            return decisionWithoutShoot[rnd.Next(0, 2)];
        }
        else if (currentMana >= _opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana &&
                 currentMana < _opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana)
        {
            return decisionWithShoot[rnd.Next(0, 3)];
        }
        else if (currentMana >= _opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana &&
                currentMana < _opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana)
        {
            return decisionWithShoot[rnd.Next(0, 4)];
        }
        else
        {
            return decisionWithShoot[rnd.Next(0, 5)];
        }
    }

}
