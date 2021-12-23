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
        if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _player.Player.IncreaseManaBar(0.1f);
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.IncreaseManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _player.Player.IncreaseManaBar(0.1f);
            _opponent.Player.getCape().SoftMagic.Activate();
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Shoot)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _player.Player.IncreaseManaBar(0.1f);
            _opponent.AttackAni();
            _player.Player.ReduceHealthBar(0.1f);
            _opponent.Player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.IncreaseManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Shoot)
        {
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().SoftMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            _opponent.Player.ReduceHealthBar(0.1f);
            _opponent.Player.IncreaseManaBar(0.1f);
            _player.Player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Shoot && opponentDecision == DecisionManager.Option.Shoot)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.Player.ReduceHealthBar(0.1f);
            _player.Player.ReduceHealthBar(0.1f);
            _player.Player.ReduceManaBar(0.1f);
            _opponent.Player.ReduceManaBar(0.1f);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Protect)
        {
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.getCape().SoftMagic.Activate();
        }
        InvokeRepeating("FinishResolving", 1.4f, 0);
    }
    void FinishResolving()
    {
        _sessionManager.ResetSession();
    }
}
