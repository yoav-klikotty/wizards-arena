using UnityEngine;
using UnityEngine.UI;

public class ResultResolver : MonoBehaviour
{
    SessionManager sessionManager;
    Wizard Player;
    Wizard Opponent;
    void Awake()
    {
        sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        Player = GameObject.Find("Player").GetComponent<Wizard>();
        Opponent = GameObject.Find("Opponent").GetComponent<Wizard>();

    }
    public void RenderDecisions(DecisionManager.Option playerDecision, DecisionManager.Option opponentDecision)
    {
        if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Reload)
        {
            Player.player.IncreaseAmmo();
            Opponent.player.IncreaseAmmo();
            Player.player.getHat().skills[0].Activate();
            Opponent.player.getHat().skills[0].Activate();
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            Player.player.getHat().skills[0].Activate();
            Opponent.player.getCape().skills[0].Activate();
            Player.player.IncreaseAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Shoot)
        {
            Player.player.getHat().skills[0].Activate();
            Player.DamageAni();
            Player.IdleAni();
            Player.player.IncreaseAmmo();
            Player.player.ReduceHealthBar(10);
            Opponent.player.ReduceAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            Player.player.getCape().skills[0].Activate();
            Opponent.player.getHat().skills[0].Activate();
            Opponent.player.IncreaseAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Shoot)
        {
            Player.player.getCape().skills[0].Activate();
            Opponent.player.ReduceAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Protect)
        {
            Player.AttackAni();
            Opponent.player.getCape().skills[0].Activate();
            Player.player.ReduceAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Reload)
        {
            Player.AttackAni();
            Opponent.DamageAni();
            Player.IdleAni();
            Opponent.IdleAni();
            Opponent.player.getHat().skills[0].Activate();
            Player.player.ReduceAmmo();
            Opponent.player.ReduceHealthBar(10);
            Opponent.player.IncreaseAmmo();
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Shoot)
        {
            Player.AttackAni();
            Opponent.AttackAni();
            Player.player.ReduceAmmo();
            Opponent.player.ReduceAmmo();
            Opponent.player.ReduceHealthBar(10);
            Player.player.ReduceHealthBar(10);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Protect)
        {
            Opponent.player.getCape().skills[0].Activate();
            Player.player.getCape().skills[0].Activate();
        }
        InvokeRepeating("FinishResolving", 3, 0);
    }

    void RenderDecision(DecisionManager.Option Decision, Wizard wizard)
    {
        if (Decision == DecisionManager.Option.Reload)
        {
            wizard.player.getHat().skills[0].Activate();
        }
        else if (Decision == DecisionManager.Option.Protect)
        {
            wizard.player.getCape().skills[0].Activate();
        }
        else
        {
            wizard.AttackAni();
        }
    }
    void FinishResolving()
    {
        sessionManager.ResetSession();
    }
}
