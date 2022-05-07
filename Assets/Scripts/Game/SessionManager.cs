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
    bool _isBotGenerateLock = false;
    bool _isDecisionLock = false;
    public List<Wizard> wizards;
    public Wizard playerWizard;
    public int MaxRankDiff = 50;
    private int _baseRankPointsChange = 10;
    private int _maxRankPointBonus = 5;
    public enum GameResult
    {
        First,
        Second,
        Third,
        Fourth,
    }
    void Start()
    {
        SoundManager.Instance.PlayBattleBackgroundSound();
        CreatePlayer();
    }
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Prefabs/Wizard/Player", new Vector3(56, 4.7f, -3), Quaternion.identity);
    }
    void Update()
    {
        RenderDecisionManger();
        if (Tweaks.BotModeActive)
        {
            CreateBots();
        }
    }

    private void UpdateMMR(GameResult wizardPlace)
    {
        int opponentsWizardsRankSum = 0;
        for(int i = 0; i < wizards.Count; i++) {
            if(wizards[i].wizardId != playerWizard.wizardId){
                opponentsWizardsRankSum += wizards[i].PlayerStatsData.RankStatsData.rank;
            }
        }
        int avgOpponentsWizardsRank = opponentsWizardsRankSum / (wizards.Count-1);
        int myRankDiff = avgOpponentsWizardsRank - playerWizard.PlayerStatsData.RankStatsData.rank;
        int myBonus = myRankDiff / (MaxRankDiff / _maxRankPointBonus);
        int rankDelta = myBonus + _baseRankPointsChange;
        playerWizard.UpdateWizardRank(wizards.Count, wizardPlace, rankDelta);
    }

    private void RenderDecisionManger()
    {
        if (wizards.Count == GameManager.Instance.NumOfPlayers && !_isDecisionLock)
        {
            _decisionManager = Instantiate(_decisionManagerPrefab, transform.root).GetComponent<DecisionManager>();
            _isDecisionLock = true;
        }
    }

    private void CreateBots()
    {
        if (wizards.Count == GameManager.Instance.ActivePlayers && PhotonNetwork.IsMasterClient && !_isBotGenerateLock)
        {
            _isBotGenerateLock = true;
            for (int i = 0; i < GameManager.Instance.NumOfPlayers - GameManager.Instance.ActivePlayers; i++)
            {
                PhotonNetwork.Instantiate("Prefabs/Wizard/WizardBot", new Vector3(56, 4.7f, -3), Quaternion.identity);
            }
        }
    }
    public void RegisterWizard(Wizard wizard)
    {
        wizards.Add(wizard);
    }
    public void ReviveWizard()
    {
        playerWizard.ReviveWizard();
    }
    public void SessionEnd()
    {
        StartCoroutine("HandleSessionEndEvent");
    }
    public IEnumerator HandleSessionEndEvent()
    {
        SoundManager.Instance.StopBattleBackgroundSound();
        wizards.Sort((wizard1, wizard2) => wizard2.Hits - wizard1.Hits);
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
    public void UpdateWizardHits(string wizardId)
    {
        var wizard = GetWizardById(wizardId);
        wizard.IncreaseHits();
    }
    public List<Wizard> GetWizardsById(string id)
    {
        var wizardsToReturn = new List<Wizard>();
        foreach (var wizard in wizards)
        {
            if (wizard.wizardId.Contains(id))
            {
                wizardsToReturn.Add(wizard);
            }
        }
        return wizardsToReturn;
    }
    public string GetRightOpponentId()
    {
        return wizards[1].wizardId;
    }
    public Wizard GetRandomOpponentId(string id)
    {
        return wizards.Find((wizard) => wizard.wizardId != id && wizard.IsWizardAlive());;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        var quittedPlayerWizards = GetWizardsById(player.UserId);
        foreach (var wizard in quittedPlayerWizards)
        {
            if (wizard.IsWizardAlive())
            {
                wizard.move = new WizardMove("game over", Vector3.forward);
                wizard.Hits = 0;
            }
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


