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
    int OpponentId;

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
            SelectOpponent(OpponentId);
        }
    }
    void Update()
    {
        if (_counter.IsCounterEnd() && _option == null)
        {
            ChooseRandom();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Opponent" && !IsDecisionMakingOver())
                {
                    OpponentId = hit.transform.gameObject.GetComponent<Wizard>().wizardId;
                    SelectOpponent(OpponentId);
                }
            }

        }
    }

    void SelectOpponent(int wizardId)
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
            elem.transform.SetParent(_btnContainer.transform);
            elem.GetComponent<Image>().sprite = magic.GetThumbnail();
            elem.GetComponent<Button>().onClick.AddListener(() => ChooseMagic(key));
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

    public void ChooseMagic(string magicName)
    {

        if (!IsDecisionMakingOver())
        {
            _option = magicName;
            player.ChooseMove(_option, OpponentId);
            foreach (Transform child in _btnContainer.transform)
            {
                var btn = child.GetComponent<Button>();
                btn.interactable = false;
            }
        }

    }

    public void ChooseRandom()
    {
        _option = "MagicChargeBlue";
        player.ChooseMove(_option, OpponentId);
    }

    public bool IsDecisionMakingOver()
    {

        return _option != null;

    }

}
