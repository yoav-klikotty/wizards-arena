using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter _counter;
    string _option;
    SessionManager _sessionManager;
    [SerializeField] Slider _manaBar;
    [SerializeField] TMP_Text _manaText;
    [SerializeField] GameObject _magicOption;
    [SerializeField] GameObject _btnContainer;
    Wizard player;
    string OpponentId;
    bool isPlayerChoseAttack;
    bool isDecisionOver;

    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        OpponentId = _sessionManager.GetRightOpponentId();
        player = _sessionManager.playerWizard;
        if (!player.IsWizardAlive())
        {
            _option = "game over";
             foreach (Transform child in _btnContainer.transform)
            {
                var btn = child.GetComponent<Button>();
                btn.interactable = false;
            }
            player.ChooseMove(_option, OpponentId);
        }
        else
        {
            player.IncreaseMana(player.WizardStatsData.GetTotalPassiveManaRegeneration());
            SetManaBar();
            UpdateValidMagicsByMana();
        }
    }
    void Update()
    {
        if (_counter.IsCounterEnd() && !IsDecisionMakingOver())
        {
            ChooseRandom();
        }
        if (isPlayerChoseAttack && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Opponent" && !IsDecisionMakingOver())
                {
                    OpponentId = hit.transform.gameObject.GetComponent<Wizard>().wizardId;
                    SelectOpponent(OpponentId);
                    player.ChooseMove(_option, OpponentId);
                    isDecisionOver = true;
                }
            }

        }
    }

    void SelectOpponent(string wizardId)
    {
        foreach (var wizard in _sessionManager.wizards)
        {
            if (wizard.wizardId == wizardId)
            {
                wizard.IsWizardSelected(true);
            }
            else
            {
                wizard.TurnWizardSelection(false);
            }
        }
    }

    void UpdateValidMagicsByMana()
    {
        foreach (string key in player.magics.Keys)
        {
            var magic = player.magics[key];
            var elem = Instantiate(_magicOption, Vector3.zero, Quaternion.identity);
            elem.transform.SetParent(_btnContainer.transform, false);
            elem.GetComponent<Image>().sprite = magic.GetThumbnail();
            elem.GetComponent<Button>().onClick.AddListener(() => ChooseMagic(key, magic.GetMagicType() == Magic.MagicType.Attack));
            if (magic.GetRequiredMana() <= player.GetMana())
            {
                elem.GetComponent<Button>().interactable = true;
            }
        }
    }
    public void SetManaBar()
    {
        _manaBar.maxValue = player.WizardStatsData.GetTotalMaxMana();
        _manaBar.value = player.GetMana();
        _manaText.text = _manaBar.value + "/" + _manaBar.maxValue;
    }

    public string GetOption()
    {

        return _option;

    }

    public void ChooseMagic(string magicName, bool isAttackMagic)
    {
        if (!IsDecisionMakingOver())
        {
            _option = magicName;
            foreach (Transform child in _btnContainer.transform)
            {
                var btn = child.GetComponent<Button>();
                btn.interactable = false;
            }
            if (!isAttackMagic)
            {
                player.ChooseMove(_option, OpponentId);
                isDecisionOver = true;
            }
            else
            {
                isPlayerChoseAttack = true;
                SelectOpponent(OpponentId);
            }
        }

    }

    public void ChooseRandom()
    {
        if (_option == null)
        {
            _option = "MagicChargeBlue";
        }
        player.ChooseMove(_option, OpponentId);
        isDecisionOver = true;
    }

    public bool IsDecisionMakingOver()
    {
        return isDecisionOver;
    }

}
