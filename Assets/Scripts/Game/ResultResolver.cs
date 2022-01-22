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
    }
    public void RenderDecisions(DecisionManager.Option playerDecision, DecisionManager.Option opponentDecision)
    {
        // easy cases
        if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.getOrb().SoftMagic.Activate();
            _player.IncreaseMana(_player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.getOrb().SoftMagic.Activate();
            _opponent.IncreaseMana(_opponent.WizardStatsData.ManaStatsData.ManaRegeneration);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Protect)
        {
            _opponent.getCape().SoftMagic.Activate();
            _player.getCape().SoftMagic.Activate();
            _opponent.ReduceMana(_opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _player.ReduceMana(_player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _player.IncreaseHealth(_player.WizardStatsData.DefenceStatsData.Recovery);
            _opponent.IncreaseHealth(_opponent.WizardStatsData.DefenceStatsData.Recovery);

        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.getOrb().SoftMagic.Activate();
            _player.IncreaseMana(_player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.getCape().SoftMagic.Activate();
            _opponent.ReduceMana(_opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _opponent.IncreaseHealth(_opponent.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.getCape().SoftMagic.Activate();
            _player.ReduceMana(_player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _opponent.getOrb().SoftMagic.Activate();
            _opponent.IncreaseMana(_opponent.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.IncreaseHealth(_player.WizardStatsData.DefenceStatsData.Recovery);
        }
        // complex cases
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.getOrb().SoftMagic.Activate();
            _opponent.getStaff().SoftMagic.Activate();
            _player.IncreaseMana(_player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            ReduceHealth(true, false, 0);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.getOrb().SoftMagic.Activate();
            _opponent.getStaff().ModerateMagic.Activate();
            _player.IncreaseMana(_player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            ReduceHealth(true, false, 0.2f);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.Reload && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.getOrb().SoftMagic.Activate();
            _opponent.getStaff().HardMagic.Activate();
            _player.IncreaseMana(_player.WizardStatsData.ManaStatsData.ManaRegeneration);
            _opponent.AttackAni();
            ReduceHealth(true, false, 0.5f);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _opponent.getStaff().SoftMagic.Activate();
            _opponent.AttackAni();
            _player.getCape().SoftMagic.Activate();
            _player.ReduceMana(_player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(true, true, 0);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _player.IncreaseHealth(_player.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _opponent.getStaff().ModerateMagic.Activate();
            _opponent.AttackAni();
            _player.getCape().SoftMagic.Activate();
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            ReduceHealth(true, true, 0.2f);
            _player.ReduceMana(_player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _player.IncreaseHealth(_player.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.Protect && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _opponent.getStaff().HardMagic.Activate();
            _opponent.AttackAni();
            _player.getCape().SoftMagic.Activate();
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            ReduceHealth(true, true, 0.5f);
            _player.ReduceMana(_player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            _player.IncreaseHealth(_player.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.getStaff().SoftMagic.Activate();
            _opponent.getCape().SoftMagic.Activate();
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0);
            _opponent.IncreaseHealth(_opponent.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.getStaff().SoftMagic.Activate();
            _player.AttackAni();
            _opponent.getOrb().SoftMagic.Activate();
            ReduceHealth(false, false, 0);
            _opponent.IncreaseMana(_opponent.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().SoftMagic.Activate();
            _opponent.getStaff().SoftMagic.Activate();
            ReduceHealth(true, false, 0);
            ReduceHealth(false, false, 0);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().SoftMagic.Activate();
            _opponent.getStaff().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.SoftAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().SoftMagic.Activate();
            _opponent.getStaff().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }

        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.getStaff().ModerateMagic.Activate();
            _opponent.getCape().SoftMagic.Activate();
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0.2f);
            _opponent.IncreaseHealth(_opponent.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.getStaff().ModerateMagic.Activate();
            _player.AttackAni();
            _opponent.getOrb().SoftMagic.Activate();
            ReduceHealth(false, false, 0.2f);
            _opponent.IncreaseMana(_opponent.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().ModerateMagic.Activate();
            _opponent.getStaff().SoftMagic.Activate();
            ReduceHealth(true, false, 0f);
            ReduceHealth(false, false, 0.2f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().ModerateMagic.Activate();
            _opponent.getStaff().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0.2f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.ModerateAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().ModerateMagic.Activate();
            _opponent.getStaff().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0.2f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Protect)
        {
            _player.AttackAni();
            _player.getStaff().HardMagic.Activate();
            _opponent.getCape().SoftMagic.Activate();
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana);
            ReduceHealth(false, true, 0.5f);
            _opponent.IncreaseHealth(_opponent.WizardStatsData.DefenceStatsData.Recovery);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.Reload)
        {
            _player.getStaff().HardMagic.Activate();
            _player.AttackAni();
            _opponent.getOrb().SoftMagic.Activate();
            ReduceHealth(false, false, 0.5f);
            _opponent.IncreaseMana(_opponent.WizardStatsData.ManaStatsData.ManaRegeneration);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.SoftAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().HardMagic.Activate();
            _opponent.getStaff().SoftMagic.Activate();
            ReduceHealth(true, false, 0f);
            ReduceHealth(false, false, 0.5f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.ModerateAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().HardMagic.Activate();
            _opponent.getStaff().ModerateMagic.Activate();
            ReduceHealth(true, false, 0.2f);
            ReduceHealth(false, false, 0.5f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana);
        }
        else if (playerDecision == DecisionManager.Option.HardAttack && opponentDecision == DecisionManager.Option.HardAttack)
        {
            _player.AttackAni();
            _opponent.AttackAni();
            _player.getStaff().HardMagic.Activate();
            _opponent.getStaff().HardMagic.Activate();
            ReduceHealth(true, false, 0.5f);
            ReduceHealth(false, false, 0.5f);
            _player.ReduceMana(_player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
            _opponent.ReduceMana(_opponent.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana);
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
                    damagePoint = CalculateDamage(_opponent.WizardStatsData, _player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
                }
                else
                {
                    damagePoint = CalculateDamage(_player.WizardStatsData, _opponent.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
                }
                if (isOpponentAttacker)
                {
                    _player.ReduceHealth(damagePoint);
                }
                else
                {
                    _opponent.ReduceHealth(damagePoint);
                }

                _photonView.RPC("ReduceHealthMulti", RpcTarget.Others, damagePoint, !isOpponentAttacker);
            }

        }
        else
        {
            int damagePoint = 0;
            if (isOpponentAttacker)
            {
                damagePoint = CalculateDamage(_opponent.WizardStatsData, _player.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
            }
            else
            {
                damagePoint = CalculateDamage(_player.WizardStatsData, _opponent.WizardStatsData, attackerMagicMultiplier, isDefenderDefends);
            }
            if (isOpponentAttacker)
            {
                _player.ReduceHealth(damagePoint);
            }
            else
            {
                _opponent.ReduceHealth(damagePoint);
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
