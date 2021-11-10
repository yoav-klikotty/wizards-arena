using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int lifeAmount;
    [SerializeField] TMP_Text LifeAmount;
    [SerializeField] int ammoAmount;
    [SerializeField] TMP_Text AmmoAmount;
    [SerializeField] Slider HealthBar;

    [SerializeField] Item wand;
    [SerializeField] Item cape;
    [SerializeField] Item hat;

    void Start()
    {
        AmmoAmount.text = ammoAmount + "";
        LifeAmount.text = lifeAmount + "";
    }
    public void ReduceAmmo()
    {
        int ammo = GetAmmo();
        if (ammo > 0)
        {
            SetAmmo(ammo - 1);
        }
    }

    public void IncreaseAmmo()
    {
        int ammo = GetAmmo() + 1;
        SetAmmo(ammo);
    }

    public void SetAmmo(int ammo)
    {
        ammoAmount = ammo;
        AmmoAmount.text = ammoAmount + "";
    }

    public int GetAmmo()
    {
        return ammoAmount;
    }

    public void SetLife(int life)
    {
        lifeAmount = life;
        LifeAmount.text = lifeAmount + "";
    }

    public int GetLife()
    {
        return lifeAmount;
    }

    public void ReduceHealthBar(int damage)
    {
        Debug.Log(damage);
        float healthBar = GetHealthBar();
        Debug.Log(healthBar);
        SetHealthBar((int)healthBar - damage);
    }

    public void SetHealthBar(int health)
    {
        HealthBar.value = health;
    }

    public float GetHealthBar()
    {
        return HealthBar.value;
    }

    public void setWand(Item wand){
        this.wand = wand;
    }
    public Item getWand(){
        return this.wand;
    }

    public void setCape(Item cape){
        this.cape = cape;
    }
    public Item getCape(){
        return this.cape;
    }
    public void setHat(Item hat){
        this.hat = hat;
    }
    public Item getHat(){
        return this.hat;
    }
}
