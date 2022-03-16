using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class SessionManager : MonoBehaviour
{
    [SerializeField] GameObject _decisionManagerPrefab;
    DecisionManager _decisionManager;
    [SerializeField] GameObject _resultResolverPrefab;
    bool _isSessionLock = false;
    bool _isSessionEndLock = false;
    bool _isDecisionLock = false;
    public List<WizardMove> moves = new List<WizardMove>();
    public List<Wizard> wizards;
    public Wizard playerWizard;
    [SerializeField] bool _isOnline;
    public enum GameResult
    {
        Lose,
        Win
    }
    void Start()
    {
        CreatePlayer();

    }
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Prefabs/Wizard/Player", new Vector3(56, 4.7f, -3), Quaternion.identity);
    }
    void Update()
    {
        if (wizards.Count == GameManager.Instance.NumOfDeathmatchPlayers && !_isDecisionLock)
        {
            _decisionManager = Instantiate(_decisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
            _isDecisionLock = true;
        }
        if (IsSidesTookDecision())
        {
            _isSessionLock = true;
            RevealDecisions();
        }
        if (wizards.Count == GameManager.Instance.NumOfDeathmatchPlayers && IsSessionEnd() && !_isSessionEndLock)
        {
            _isSessionEndLock = true;
            StartCoroutine("HandleSessionEndEvent");
        }
    }


    bool IsSidesTookDecision()
    {
        if (moves.Count == 0)
        {
            return false;
        }
        bool isContainNoneDecision = false;
        foreach (WizardMove wizardMove in moves)
        {
            if (wizardMove.wizardOption == null)
            {
                isContainNoneDecision = true;
            }
        }
        if (!isContainNoneDecision &&
            !_isSessionLock)
        {
            return true;
        }
        return false;
    }

    public void RegisterWizard(Wizard wizard)
    {
        wizards.Add(wizard);
        moves.Add(new WizardMove(null, Vector3.zero));
    }

    public void RegisterWizardMove(int wizardIndex, WizardMove move)
    {
        moves[wizardIndex] = move;
    }

    void RevealDecisions()
    {
        Destroy(_decisionManager.gameObject);
        RenderDecisions();
    }

    void RenderDecisions()
    {
        for (int i = 0; i < moves.Count; i++)
        {
            wizards[i].RenderDecision(moves[i]);
        }
        for (int i = 0; i < moves.Count; i++)
        {
            moves[i] = new WizardMove(null, Vector3.zero);
        }
        InvokeRepeating("FinishResolving", 1.5f, 0);
    }
    void FinishResolving()
    {
        ResetSession();
    }

    public void ResetSession()
    {
        if (_decisionManager == null)
        {
            _decisionManager = Instantiate(_decisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
        }
        _isSessionLock = false;
    }

    bool IsSessionEnd()
    {
        int numberOfAliveWizards = wizards.Count;
        foreach(var wizard in wizards)
        {
            if (!wizard.IsWizardAlive())
            {
                numberOfAliveWizards -= 1;
            }
        }
        if (numberOfAliveWizards < 2)
        {
            return true;
        }
        else {
            return false;
        }
    }
    IEnumerator HandleSessionEndEvent()
    {
        var myWizard = wizards.Find(wiz => wiz.wizardId == playerWizard.wizardId);
        if (myWizard.IsWizardAlive())
        {
            LocalStorage.SetLastSessionResult(GameResult.Win);
        }
        else 
        {
            LocalStorage.SetLastSessionResult(GameResult.Lose);
        }
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Score");
    }

    public Wizard GetWizardById(int id)
    {
        return wizards.Find((wizard) => wizard.wizardId == id);
    }
    public int GetRightOpponentId()
    {
        return wizards[1].wizardId;
    }
}

public class WizardMove
{
    public string wizardOption;
    public Vector3 wizardOpponentPosition;

    public WizardMove(string wizardOption, Vector3 wizardOppoentPosition)
    {
        this.wizardOption = wizardOption;
        this.wizardOpponentPosition = wizardOppoentPosition;
    }

}


