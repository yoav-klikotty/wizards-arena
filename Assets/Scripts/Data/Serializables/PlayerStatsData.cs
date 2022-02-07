using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatsData
{
    public string _name = "";
    public int _level = 30;
    public int _levelPoints = 0;
    public int _coins = 100000;
    public int _crystals = 5;
    public int _maxCrystals = 20;
    public string GetName()
    {
        return _name;
    }

    public void SetName(string _name)
    {
        this._name = _name;
    }

    public int GetLevel()
    {
        return _level;
    }

    public void UpgradeLevel()
    {
        this._level += 1;
    }

    public int GetLevelPoints()
    {
        return this._levelPoints;
    }

    public void AddLevelPoints(int levelPoints)
    {
        for (var i = 0; i < levelPoints; i++)
        {
            if (this._levelPoints == 99)
            {
                UpgradeLevel();
                this._levelPoints = 0;
            }
            else
            {
                this._levelPoints += 1;
            }
        }
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

}
