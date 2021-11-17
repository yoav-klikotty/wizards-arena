using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaterStats : MonoBehaviour
{
    [SerializeField] TMP_Text nameObj;
    [SerializeField] TMP_Text levelObj;
    [SerializeField] TMP_Text coinsObj;
    [SerializeField] TMP_Text energyObj;

    // defautls
    public int level;
    public int energy;
    public int coins;

    void Start()
    {   
        // var playerData = getData
        
        nameObj.text = "testPlayer";
        // nameObj.text = playerData.name

        coinsObj.text = coins.ToString();
        // coinsObj.text = playerData.coins

        energyObj.text = energy + "/20";
        // energyObj.text = playerData.energy + "/" + playerData.maxEnergy

        levelObj.text = "level: " + level;
        // levelObj.text = "level: " + playerData.level;
    }

    public void AddCoins (int num) {
        coins += num;
        coinsObj.text = coins.ToString();
    }
    public void LevelUp (int num) {
        level++;
        levelObj.text = "level: " + level;
    }
    public void AddEnergy (int num) {
        energy += num;
        energyObj.text = energy + "/20";
        // energyObj.text = level + "/" + playerData.maxEnergy;
    }
}
