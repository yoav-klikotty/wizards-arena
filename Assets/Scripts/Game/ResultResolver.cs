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
            Player.player.getHat().skills[0].Activate();
            Player.player.IncreaseManaBar(0.1f);
            Opponent.player.getHat().skills[0].Activate();
            Opponent.player.IncreaseManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            Player.player.getHat().skills[0].Activate();
            Player.player.IncreaseManaBar(0.1f);
            Opponent.player.getCape().skills[0].Activate();
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Shoot)
        {
            Player.player.getHat().skills[0].Activate();
            Opponent.player.getWand().skills[0].ActivateFirePrefab();
            Player.player.IncreaseManaBar(0.1f);
            Opponent.AttackAni();
            Player.player.ReduceHealthBar(0.1f);
            Opponent.player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            Player.player.getCape().skills[0].Activate();
            Opponent.player.getHat().skills[0].Activate();
            Opponent.player.IncreaseManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Shoot)
        {
            Opponent.player.getWand().skills[0].ActivateFirePrefab();
            Opponent.AttackAni();
            Player.player.getCape().skills[0].Activate();
            Opponent.player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Protect)
        {
            Player.AttackAni();
            Player.player.getWand().skills[0].ActivateFirePrefab();
            Opponent.player.getCape().skills[0].Activate();
            Player.player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Reload)
        {
            Player.player.getWand().skills[0].ActivateFirePrefab();
            Player.AttackAni();
            Opponent.player.getHat().skills[0].Activate();
            Opponent.player.ReduceHealthBar(0.1f);
            Opponent.player.IncreaseManaBar(0.1f);
            Player.player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Shoot)
        {
            Player.AttackAni();
            Opponent.AttackAni();
            Player.player.getWand().skills[0].ActivateFirePrefab();
            Opponent.player.getWand().skills[0].ActivateFirePrefab();
            Opponent.player.ReduceHealthBar(0.1f);
            Player.player.ReduceHealthBar(0.1f);
            Player.player.ReduceManaBar(0.1f);
            Opponent.player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Protect)
        {
            Opponent.player.getCape().skills[0].Activate();
            Player.player.getCape().skills[0].Activate();
        }
        InvokeRepeating("FinishResolving", 1.4f, 0);
    }
    void FinishResolving()
    {
        sessionManager.ResetSession();
    }
}
