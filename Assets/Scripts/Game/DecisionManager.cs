using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter _counter;
    [SerializeField] Option _option;
    Syncronizer _syncronizer;
    [SerializeField] Button _softAttackMagic;
    [SerializeField] Button _moderateAttackMagic;
    [SerializeField] Button _hardAttackMagic;

    [SerializeField] Button _ammoBtn;
    [SerializeField] Button _shieldBtn;
    [SerializeField] Slider _manaBar;
    [SerializeField] TMP_Text _manaText;

    Player player;
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
        _syncronizer = GameObject.Find("Syncronizer").GetComponent<Syncronizer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        SetManaBar();
        CalculateValidAttacks();
    }
    void Update()
    {
        if (_counter.IsCounterEnd() && _option == Option.None)
        {
            ChooseRandom();
        }
    }

    void CalculateValidAttacks()
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
    }
    public void SetManaBar()
    {
        _manaBar.maxValue = player.WizardStatsData.ManaStatsData.MaxMana;
        _manaBar.value = player.GetMana();
        _manaText.text = _manaBar.value + "/" +  _manaBar.maxValue;
    }

    public Option GetOption()
    {

        return _option;

    }

    public void ChooseAmmo()
    {

        if (!IsDecisionMakingOver())
        {
            _syncronizer.UpdatePlayersDecision(Option.Reload);
            _option = Option.Reload;
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
            _syncronizer.UpdatePlayersDecision(Option.Protect);
            _option = Option.Protect;
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
            _syncronizer.UpdatePlayersDecision(Option.SoftAttack);
            _option = Option.SoftAttack;
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
            _syncronizer.UpdatePlayersDecision(Option.ModerateAttack);
            _option = Option.ModerateAttack;
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
            _syncronizer.UpdatePlayersDecision(Option.HardAttack);
            _option = Option.HardAttack;
            _ammoBtn.interactable = false;
            _shieldBtn.interactable = false;
            _softAttackMagic.interactable = false;
            _moderateAttackMagic.interactable = false;
        }
    }

    public void ChooseRandom()
    {

        _syncronizer.UpdatePlayersDecision(Option.Reload);
        _option = Option.Reload;

    }

    public bool IsDecisionMakingOver()
    {

        return _option != Option.None;

    }

}
