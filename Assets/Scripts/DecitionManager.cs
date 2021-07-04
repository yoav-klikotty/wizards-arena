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
    [SerializeField] MasterStatus masterStatus;
    [SerializeField] SlaveStatus slaveStatus;

    [SerializeField] Button shootBtn;

    [SerializeField] Button ammoBtn;
    [SerializeField] Button shieldBtn;

     public enum Option{
        Ammo,
        Shield,
        Shoot,
        Random
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.IsCounterEnd() && option == Option.Random){
            ChooseRandom();
        }
        if (PhotonNetwork.IsMasterClient)
        {
            if (masterStatus.GetAmmo() == 0){
                shootBtn.interactable = false;
            }
            else {
                if (!IsDecitionMakingOver()){
                    shootBtn.interactable = true;
                }
            }
        }
        else {
            if (slaveStatus.GetAmmo() == 0){
                shootBtn.interactable = false;
            }
            else {
                if (!IsDecitionMakingOver()){
                    shootBtn.interactable = true;
                }
            }
        }
    }
    public Option GetOption(){
        return option;
    }
    public void ChooseAmmo(){
        if (!IsDecitionMakingOver()){
            option = Option.Ammo;
            shootBtn.interactable = false;
            shieldBtn.interactable = false;
        }

    }

    public void ChooseShield(){
        if (!IsDecitionMakingOver()){
            option = Option.Shield;
            ammoBtn.interactable = false;
            shootBtn.interactable = false;
        }
    }
    public void ChooseShoot(){
        if (!IsDecitionMakingOver()){
            option = Option.Shoot;
            ammoBtn.interactable = false;
            shieldBtn.interactable = false;
        }
    }
    public void ChooseRandom(){
        option = Option.Ammo;
    }

    public bool IsDecitionMakingOver(){
        return option != Option.Random;
    }

    public void ResetDecitionManager(){
        counter.ResetCounter();
        option = Option.Random;
        ammoBtn.interactable = true;
        shieldBtn.interactable = true;
        shootBtn.interactable = true;
    }
}
