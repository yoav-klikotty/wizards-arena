using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter _counter;
    [SerializeField] Option _option;
    SessionManager _sessionManager;
    [SerializeField] Button _softAttackMagic;
    [SerializeField] Button _moderateAttackMagic;
    [SerializeField] Button _hardAttackMagic;

    [SerializeField] Button _ammoBtn;
    [SerializeField] Button _shieldBtn;
    [SerializeField] Slider _manaBar;
    [SerializeField] TMP_Text _manaText;
    Wizard player;
    int OpponentId;
    public enum Option
    {
        Reload,
        Protect,
        SoftAttack,
        ModerateAttack,
        HardAttack,
        None
    }

    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        OpponentId = _sessionManager.GetRightOpponentId();
        player = _sessionManager.playerWizard;
        player.IncreaseMana(player.WizardStatsData.GetTotalPassiveManaRegeneration());
        SetManaBar();
        UpdateValidMagicsByMana();
        SelectOpponent();
    }
    void Update()
    {
        if (_counter.IsCounterEnd() && _option == Option.None)
        {
            ChooseRandom();
        }
    }

    void SelectOpponent()
    {

    }

    void UpdateValidMagicsByMana()
    {
        if (player.WizardStatsData.StaffStatsData.SoftMagicStats.requiredMana <= player.GetMana())
        {
            _softAttackMagic.interactable = true;
        }
        else
        {
            _softAttackMagic.interactable = false;
        }
        if (player.WizardStatsData.StaffStatsData.ModerateMagicStats.requiredMana <= player.GetMana())
        {
            _moderateAttackMagic.interactable = true;
        }
        else
        {
            _moderateAttackMagic.interactable = false;
        }
        if (player.WizardStatsData.StaffStatsData.HardMagicStats.requiredMana <= player.GetMana())
        {
            _hardAttackMagic.interactable = true;
        }
        else
        {
            _hardAttackMagic.interactable = false;
        }
        if (player.WizardStatsData.CapeStatsData.SoftMagicStats.requiredMana <= player.GetMana())
        {
            _shieldBtn.interactable = true;
        }
        else
        {
            _shieldBtn.interactable = false;
        }
    }
    public void SetManaBar()
    {
        _manaBar.maxValue = player.WizardStatsData.GetTotalMaxMana();
        _manaBar.value = player.GetMana();
        _manaText.text = _manaBar.value + "/" + _manaBar.maxValue;
    }

    public Option GetOption()
    {

        return _option;

    }

    public void ChooseAmmo()
    {

        if (!IsDecisionMakingOver())
        {
            _option = Option.Reload;
            player.ChooseMove(_option, OpponentId);
            _softAttackMagic.interactable = false;
            _moderateAttackMagic.interactable = false;
            _hardAttackMagic.interactable = false;
            _shieldBtn.interactable = false;
        }

    }

    public void ChooseShield()
    {

        if (!IsDecisionMakingOver())
        {

            _option = Option.Protect;
            player.ChooseMove(_option, OpponentId);

            _ammoBtn.interactable = false;
            _softAttackMagic.interactable = false;
            _moderateAttackMagic.interactable = false;
            _hardAttackMagic.interactable = false;
        }

    }

    public void ChooseSoftAttack()
    {

        if (!IsDecisionMakingOver())
        {
            _option = Option.SoftAttack;
            player.ChooseMove(_option, OpponentId);

            _ammoBtn.interactable = false;
            _shieldBtn.interactable = false;
            _moderateAttackMagic.interactable = false;
            _hardAttackMagic.interactable = false;
        }
    }

    public void ChooseModerateAttack()
    {

        if (!IsDecisionMakingOver())
        {
            _option = Option.ModerateAttack;
            player.ChooseMove(_option, OpponentId);
            _ammoBtn.interactable = false;
            _shieldBtn.interactable = false;
            _softAttackMagic.interactable = false;
            _hardAttackMagic.interactable = false;
        }
    }

    public void ChooseHardAttack()
    {

        if (!IsDecisionMakingOver())
        {
            _option = Option.HardAttack;
            player.ChooseMove(_option, OpponentId);
            _ammoBtn.interactable = false;
            _shieldBtn.interactable = false;
            _softAttackMagic.interactable = false;
            _moderateAttackMagic.interactable = false;
        }
    }

    public void ChooseRandom()
    {
        _option = Option.Reload;
        player.ChooseMove(_option, OpponentId);
    }

    public void ChooseLeftPlayer()
    {

        OpponentId = _sessionManager.GetLeftOpponentId();

    }

    public void ChooseRightPlayer()
    {
        OpponentId = _sessionManager.GetRightOpponentId();

    }

    public bool IsDecisionMakingOver()
    {

        return _option != Option.None;

    }

}
