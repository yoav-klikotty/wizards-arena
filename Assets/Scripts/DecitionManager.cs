using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DecitionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Counter counter;
    [SerializeField] Option option;
    Player playerStatus;
    Syncronizer syncronizer;
    [SerializeField] Button shootBtn;

    [SerializeField] Button ammoBtn;
    [SerializeField] Button shieldBtn;

     public enum Option{
        Reload,
        Protect,
        Shoot,
        None
    }

    void Start()
    {
        playerStatus = GameObject.Find("PlayerStatus").GetComponent<Player>();
        syncronizer = GameObject.Find("Syncronizer").GetComponent<Syncronizer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.IsCounterEnd() && option == Option.None)
        {
            ChooseRandom();
        }
        if (playerStatus.GetAmmo() == 0)
        {
            shootBtn.interactable = false;
        }
        else
        {
            if (!IsDecitionMakingOver())
            {
                shootBtn.interactable = true;
            }
        }
    }

    public Option GetOption(){

        return option;

    }

    public void ChooseAmmo(){

        if (!IsDecitionMakingOver()){
            syncronizer.HandleUpdateForOnlineGame(Option.Reload);
            option = Option.Reload;
            shootBtn.interactable = false;
            shieldBtn.interactable = false;
        }

    }

    public void ChooseShield(){

        if (!IsDecitionMakingOver()){
            syncronizer.HandleUpdateForOnlineGame(Option.Protect);
            option = Option.Protect;
            ammoBtn.interactable = false;
            shootBtn.interactable = false;
        }

    }

    public void ChooseShoot(){

        if (!IsDecitionMakingOver()){
            syncronizer.HandleUpdateForOnlineGame(Option.Shoot);
            option = Option.Shoot;
            ammoBtn.interactable = false;
            shieldBtn.interactable = false;
        }
    }

    public void ChooseRandom(){

        syncronizer.HandleUpdateForOnlineGame(Option.Reload);
        option = Option.Reload;

    }

    public bool IsDecitionMakingOver(){

        return option != Option.None;

    }

}
