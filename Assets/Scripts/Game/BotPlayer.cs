using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BotPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Wizard _opponent;
    public string GetBotDecision()
    {
        _opponent = GameObject.Find("Opponent").GetComponent<Wizard>();
        List <string> decitions = new List<string>();
        Random rnd = new Random();
        int currentMana = _opponent.GetMana();
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
