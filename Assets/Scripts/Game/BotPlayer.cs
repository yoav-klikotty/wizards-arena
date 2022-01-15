using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BotPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player _opponent;
    public DecisionManager.Option GetBotDecision()
    {
        List <DecisionManager.Option> decitions = new List<DecisionManager.Option>();
        Random rnd = new Random();
        int currentMana = _opponent.GetMana();
        decitions.Add(DecisionManager.Option.Protect);
        // if (currentMana >= _opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana)
        // {
        //     decitions.Add(DecisionManager.Option.Protect);
        // }
        // if (currentMana >= _opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana)
        // {
        //     decitions.Add(DecisionManager.Option.SoftAttack);
        // }
        // if (currentMana >= _opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana)
        // {
        //     decitions.Add(DecisionManager.Option.ModerateAttack);
        // }
        // if (currentMana >= _opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana)
        // {
        //     decitions.Add(DecisionManager.Option.HardAttack);
        // }
        return decitions[rnd.Next(0, decitions.Count - 1)];
    }

}
