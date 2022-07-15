using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatsData
{
    public RankStatsData RankStatsData = new RankStatsData(0);
    public string _name = "";
    public int _coins = 50000;
    public int _crystals = 20;
    public int _maxCrystals = 20;
    public int _level = 10;
    public int _xp = 0;
    public Dictionary<int, int> _maxXP = new Dictionary<int, int>(){
        {1, 15},
        {2, 37},
        {3, 70},
        {4, 115},
        {5, 169},
        {6, 231},
        {7, 305},
        {8, 384},
        {9, 474},
        {10, 569},
        {11, 672}
    };
    public int _masteriesPoints = 1;
    public string GetName()
    {
        return _name;
    }

    public void SetName(string _name)
    {
        this._name = _name;
    }
    public int GetXP()
    {
        return this._xp;
    }
    public int GetMaxXP()
    {
        return _maxXP[this._level];
    }
    public void AddXP(int xp)
    {
        int maxXP = _maxXP[this._level];
        for (var i = 0; i < xp; i++)
        {
            if (this._xp == maxXP)
            {
                UpgradeLevel();
                this._xp = 0;
            }
            else
            {
                this._xp += 1;
            }
        }
    }
    public int GetLevel()
    {
        return _level;
    }

    public void UpgradeLevel()
    {
        this._level += 1;
        IncreaseMasteriesPoints(1);
        EventManager.Instance.LevelUpgrade();
    }

    public int GetCoins()
    {
        return this._coins;
    }

    public void SetCoins(int _coins)
    {
        this._coins = _coins;
    }

    public int GetCrystals()
    {
        return this._crystals;
    }

    public void SetCrystals(int _crystals)
    {
        if (_crystals > this._maxCrystals)
        {
            this._crystals = this._maxCrystals;
        }
        else
        {
            this._crystals = _crystals;
        }
    }
    public int GetMaxCrystals()
    {
        return this._maxCrystals;
    }

    public int GetMasteriesPoints()
    {
        return this._masteriesPoints;
    }
    public void ReduceMasteriesPoints()
    {
        if (_masteriesPoints > 0)
        {
            this._masteriesPoints -= 1;
        }
    }
    private void IncreaseMasteriesPoints(int masteryPoint)
    {
        this._masteriesPoints += masteryPoint;
    }
  
}

[Serializable]
public class RankStatsData
{
    public RankStatsData(int initialRank)
    {
        this.rank = initialRank;
    }
    public void AddRank(int rankDiff)
    {
        this.rank += rankDiff;
        if (this.rank < 0) {
            this.rank = 0;
        }
    }
    public int rank;
}