using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] TMP_Text _nameInput;
    [SerializeField] TMP_Text _levelInput;
    [SerializeField] TMP_Text _coinsInput;
    [SerializeField] TMP_Text _energyInput;

    // defautls
    public int Level;
    public int Energy;
    public int Coins;

    void Start()
    {   
        // var playerData = getData;
        
        _nameInput.text = "testPlayer";
        // _nameInput.text = playerData.name;

        _coinsInput.text = Coins.ToString();
        // _coinsInput.text = playerData.coins;

        _energyInput.text = Energy + "/20";
        // _energyInput.text = playerData.energy + "/" + playerData.maxEnergy;

        _levelInput.text = "level: " + Level;
        // _levelInput.text = "level: " + playerData.level;
    }

    public void AddCoins (int num) {
        Coins += num;
        _coinsInput.text = Coins.ToString();
    }
    public void LevelUp (int num) {
        Level++;
        _levelInput.text = "level: " + Level;
    }
    public void AddEnergy (int num) {
        Energy += num;
        _energyInput.text = Energy + "/20";
        // _energyInput.text = level + "/" + playerData.maxEnergy;
    }
}
