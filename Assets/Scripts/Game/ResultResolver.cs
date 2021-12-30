using UnityEngine;
using UnityEngine.UI;

public class ResultResolver : MonoBehaviour
{
    SessionManager _sessionManager;
    Wizard _player;
    Wizard _opponent;
    void Awake()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        _player = GameObject.Find("Player").GetComponent<Wizard>();
        _opponent = GameObject.Find("Opponent").GetComponent<Wizard>();

    }
    public void RenderDecisions(DecisionManager.Option playerDecision, DecisionManager.Option opponentDecision)
    {
        // easy cases
        if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Protect)
        {
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.getCape().SoftMagic.Activate();
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.Player.getCape().SoftMagic.Activate();
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
        }
        // complex cases
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            _player.Player.ReduceHealth(10);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            _player.Player.ReduceHealth(20);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            _player.Player.ReduceHealth(30);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _opponent.Player.getWand().ModerateMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _opponent.Player.getWand().HardMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().SoftMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(10);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(10);
            _player.Player.ReduceHealth(10);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.ReduceHealth(20);
            _player.Player.ReduceHealth(10);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            _opponent.Player.ReduceHealth(30);
            _player.Player.ReduceHealth(10);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

         else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().ModerateMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(20);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(20);
            _player.Player.ReduceHealth(10);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.ReduceHealth(20);
            _player.Player.ReduceHealth(20);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            _opponent.Player.ReduceHealth(20);
            _player.Player.ReduceHealth(30);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
         else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().HardMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(30);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.Player.ReduceHealth(10);
            _player.Player.ReduceHealth(30);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.ReduceHealth(30);
            _player.Player.ReduceHealth(20);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            _opponent.Player.ReduceHealth(30);
            _player.Player.ReduceHealth(30);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        
        InvokeRepeating("FinishResolving", 1.4f, 0);
    }
    void FinishResolving()
    {
        _sessionManager.ResetSession();
    }
}
