using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using Photon.Pun;


public class ResultResolver : MonoBehaviour
{
    SessionManager _sessionManager;
    Wizard _player;
    Wizard _opponent;
    PhotonView _photonView;
    void Awake()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        _player = GameObject.Find("Player").GetComponent<Wizard>();
        _opponent = GameObject.Find("Opponent").GetComponent<Wizard>();
        _photonView = GameObject.Find("Syncronizer").GetComponent<PhotonView>();
        Debug.Log(_photonView.ViewID);

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
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);

        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
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
            ReduceHealth(true, false, 0);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            ReduceHealth(true, false, 0.2f);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.Player.getHat().SoftMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            _player.Player.IncreaseMana(_player.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            ReduceHealth(true, false, 0.5f);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _opponent.Player.getWand().SoftMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(true, true, 0);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _opponent.Player.getWand().ModerateMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            ReduceHealth(true, true, 0.2f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _opponent.Player.getWand().HardMagic.Activate();
            _opponent.AttackAni();
            _player.Player.getCape().SoftMagic.Activate();
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            ReduceHealth(true, true, 0.5f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().SoftMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            ReduceHealth(false, false, 0);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            ReduceHealth(true, false, 0);
            ReduceHealth(false, false, 0);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().SoftMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0.2f);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().ModerateMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            ReduceHealth(false, false, 0.2f);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            ReduceHealth(true, false, 0f);
            ReduceHealth(false, false, 0.2f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0.2f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().ModerateMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0.2f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getCape().SoftMagic.Activate();
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0.5f);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.Player.getWand().HardMagic.Activate();
            _player.AttackAni();
            _opponent.Player.getHat().SoftMagic.Activate();
            ReduceHealth(false, false, 0.5f);
            _opponent.Player.IncreaseMana(_opponent.Player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().SoftMagic.Activate();
            ReduceHealth(true, false, 0f);
            ReduceHealth(false, false, 0.5f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0.5f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.Player.getWand().HardMagic.Activate();
            _opponent.Player.getWand().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0.5f);
            _player.Player.ReduceMana(_player.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.Player.ReduceMana(_opponent.Player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

        InvokeRepeating("FinishResolving", 1.5f, 0);
    }

    void ReduceHealth(bool isOpponentAttacker, bool isDefenderDefends, float attackerMagicMultiplier)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int damagePoint = 0;
                if (isOpponentAttacker)
                {
                    damagePoint = CalculateDamage(_opponent.Player.WizardStatsData, _player.Player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
                }
                else
                {
                    damagePoint = CalculateDamage(_player.Player.WizardStatsData, _opponent.Player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
                }
                if (isOpponentAttacker)
                {
                    _player.Player.ReduceHealth(damagePoint);
                }
                else
                {
                    _opponent.Player.ReduceHealth(damagePoint);
                }

                _photonView.RPC("ReduceHealthMulti", RpcTarget.Others, damagePoint, !isOpponentAttacker);
            }

        }
        else
        {
            int damagePoint = 0;
            if (isOpponentAttacker)
            {
                damagePoint = CalculateDamage(_opponent.Player.WizardStatsData, _player.Player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
            }
            else
            {
                damagePoint = CalculateDamage(_player.Player.WizardStatsData, _opponent.Player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
            }
            if (isOpponentAttacker)
            {
                _player.Player.ReduceHealth(damagePoint);
            }
            else
            {
                _opponent.Player.ReduceHealth(damagePoint);
            }
        }
    }

    int CalculateDamage(WizardStatsData attacker, WizardStatsData defender, float attackerMagicMultiplier, bool isDefenderDefends)
    {
        Random rnd = new Random();
        if (!isDefenderDefends)
        {
            if (rnd.NextDouble() <= defender.DefenceStatsData.Avoidability)
            {
                return 0;
            }
        }
        int damagePoints = 0;
        int baseDamage = rnd.Next(attacker.AttackStatsData.MinBaseDamage, attacker.AttackStatsData.MaxBaseDamage);
        damagePoints += baseDamage;
        bool isCriticalHit = rnd.NextDouble() <= attacker.AttackStatsData.CriticalRate;
        if (isCriticalHit)
        {
            damagePoints += (int)((attacker.AttackStatsData.CriticalDmg + attackerMagicMultiplier) * baseDamage);
        }
        else
        {
            damagePoints += (int)((attackerMagicMultiplier) * baseDamage);
        }
        if (isDefenderDefends)
        {
            damagePoints = (int)(attackerMagicMultiplier * damagePoints);
        }
        return damagePoints;
    }

    void FinishResolving()
    {
        _sessionManager.ResetSession();
    }
}
