using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections;

public class DecisionManager : MonoBehaviour
{
    string _option;
    SessionManager _sessionManager;
    [SerializeField] TMP_Text _manaText;
    [SerializeField] GameObject _magicOption;
    [SerializeField] GameObject _btnContainer;
    [SerializeField] GameObject _reviveButton;
    Wizard player;
    string OpponentId;
    bool isPlayerChoseAttack;
    bool isDecisionOver;
    bool reviveLock;

    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        player = _sessionManager.playerWizard;
        OpponentId = _sessionManager.GetRandomOpponentId(player.wizardId).wizardId;
        player.IncreaseMana(player.WizardStatsData.GetTotalPassiveManaRegeneration());
        UpdateValidMagicsByMana();
    }
    void Update()
    {
        if (player.IsWizardAlive())
        {
            reviveLock = false;
            _reviveButton.SetActive(false);
        }
        else if (!reviveLock)
        {
            reviveLock = true;
            StartCoroutine(RenderReviveButton());
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
                    isPlayerChoseAttack = false;
                    StartCoroutine(EnableDecision());

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

    public string GetOption()
    {

        return _option;

    }

    public void ChooseMagic(string magicName, bool isAttackMagic)
    {
        if (_sessionManager.playerWizard.IsWizardAlive())
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
                StartCoroutine(EnableDecision());

            }
            else
            {
                isPlayerChoseAttack = true;
                SelectOpponent(OpponentId);
            }
        }

    }
    IEnumerator RenderReviveButton()
    {
        yield return new WaitForSeconds(3);
        _reviveButton.SetActive(true);
    }

    IEnumerator EnableDecision()
    {
        yield return new WaitForSeconds(1);
        isDecisionOver = false;
        int childs = _btnContainer.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            GameObject.Destroy(_btnContainer.transform.GetChild(i).gameObject);
        }
        UpdateValidMagicsByMana();
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
    public void ReviveWizard()
    {
        this._sessionManager.ReviveWizard();
    }

}
