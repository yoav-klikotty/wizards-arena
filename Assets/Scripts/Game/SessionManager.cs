using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System;

public class SessionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _decisionManagerPrefab;
    DecisionManager _decisionManager;
    bool _isSessionLock = false;
    bool _isSessionEndLock = false;
    bool _isDecisionLock = false;
    public List<Wizard> wizards;
    public Wizard playerWizard;
    public int maxRankDiff = 50;
    private int _baseRankPointsChange = 10;
    private int _maxRankPointBonus = 5;
    public enum GameResult
    {
        First,
        Second,
        Third,
        Forth,
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

    private void UpdateMMR(GameResult wizardPlace)
    {
        int opponentsWizardsRankSum = 0;
        for(int i = 0; i < wizards.Count; i++) {
            if(wizards[i].wizardId != playerWizard.wizardId){
                opponentsWizardsRankSum += wizards[i].WizardStatsData.RankStatsData.GetModeRank(wizards.Count);
            }
        }
        int avgOpponentsWizardsRank = opponentsWizardsRankSum / (wizards.Count-1);
        int myRankDiff = avgOpponentsWizardsRank - playerWizard.WizardStatsData.RankStatsData.GetModeRank(wizards.Count);
        int myBonus = myRankDiff / (maxRankDiff / _maxRankPointBonus);
        int rankDelta = myBonus + _baseRankPointsChange;
        playerWizard.updateWizardRank(wizards.Count, wizardPlace, rankDelta);
    }

    bool IsSidesTookDecision()
    {
        if (wizards.Count == 0)
        {
            return false;
        }
        bool isContainNoneDecision = false;
        foreach (Wizard wizard in wizards)
        {
            if (wizard.move.wizardOption == null)
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
    }
    void RevealDecisions()
    {
        Destroy(_decisionManager.gameObject);
        RenderDecisions();
    }

    void RenderDecisions()
    {
        for (int i = 0; i < wizards.Count; i++)
        {
            wizards[i].RenderDecision();
        }
        InvokeRepeating("FinishResolving", 1.5f, 0);
    }
    void FinishResolving()
    {
        ResetSession();
    }

    public void ResetSession()
    {
        if (_decisionManager == null && !IsSessionEnd())
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
        wizards.ForEach(wizard => {
            if (wizard.IsWizardAlive())
            {
                wizard.DeathTime = DateTime.Now;
            }
        });
        wizards.Sort((wizard1, wizard2) => DateTime.Compare(wizard2.DeathTime, wizard1.DeathTime));
        var myWizard = wizards.FindIndex(wizard => wizard.wizardId == playerWizard.wizardId);
        LocalStorage.SetLastSessionResult((GameResult)myWizard);
        UpdateMMR((GameResult)myWizard);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Score");
    }

    public Wizard GetWizardById(string id)
    {
        return wizards.Find((wizard) => wizard.wizardId == id);
    }
    public string GetRightOpponentId()
    {
        return wizards[1].wizardId;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        var quittedPlayer = GetWizardById(player.UserId);
        if (quittedPlayer.IsWizardAlive())
        {
            quittedPlayer.move = new WizardMove("game over", Vector3.forward);
            quittedPlayer.DeathTime = DateTime.Now;
        }

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


