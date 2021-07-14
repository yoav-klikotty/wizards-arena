using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class SlaveStatus : MonoBehaviour
{

    int lifeAmount = 2;
    [SerializeField] TMP_Text LifeAmount;
    int ammoAmount = 0;
    [SerializeField] TMP_Text AmmoAmount;
    int healthBarAmmount = 5;
    [SerializeField] Slider HealthBar;
    PhotonView photonView;
    [SerializeField] Image background;

    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        if (!PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetAmmo", RpcTarget.All, ammoAmount);
            photonView.RPC("SetLife", RpcTarget.All, lifeAmount);
            photonView.RPC("SetHealthBar", RpcTarget.All, healthBarAmmount);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBackgroud(){
          Color tempColor = background.color;
          tempColor.a = 1f;
          background.color = tempColor;
    }
    public void ReduceAmmo(){
        if (!PhotonNetwork.IsMasterClient)
        {
            int ammo = GetAmmo();
            if (ammo > 0){
                photonView.RPC("SetAmmo", RpcTarget.All, ammo - 1);
            }
        }
    }

    public void IncreaseAmmo(){
        if (!PhotonNetwork.IsMasterClient)
        {
            int ammo = GetAmmo();
            photonView.RPC("SetAmmo", RpcTarget.All, ammo + 1);
        }
    }

    [PunRPC]
    public void SetAmmo(int ammo)
    {
        ammoAmount = ammo;
        AmmoAmount.text = ammoAmount + "";
    }

    [PunRPC]
    public int GetAmmo()
    {
        return ammoAmount;
    }

    [PunRPC]
    public void SetLife(int life)
    {
        lifeAmount = life;
        LifeAmount.text = lifeAmount + "";
    }

    [PunRPC]
    public int GetLife()
    {
        return lifeAmount;
    }
    public void ReduceHealthBar(){
        if (!PhotonNetwork.IsMasterClient)
        {
            float healthBar = GetHealthBar();
            photonView.RPC("SetHealthBar", RpcTarget.All, (int)healthBar - 1);
        }
    }

    [PunRPC]
    public void SetHealthBar(int health)
    {
        HealthBar.value = health;
    }

    [PunRPC]
    public float GetHealthBar()
    {
        return HealthBar.value;
    }
}
