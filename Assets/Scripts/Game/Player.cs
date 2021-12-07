using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider HealthBar;
    [SerializeField] Slider ManaBar;

    [SerializeField] Item wand;
    [SerializeField] Item cape;
    [SerializeField] Item hat;

    public void ReduceHealthBar(float damage)
    {
        float healthBar = GetHealthBar();
        SetHealthBar(healthBar - damage);
    }

    public void SetHealthBar(float health)
    {
        HealthBar.value = health;
    }

    public float GetHealthBar()
    {
        return HealthBar.value;
    }

    public void ReduceManaBar(float mana)
    {
        float manaBar = GetManaBar();
        SetManaBar(manaBar - mana);
    }

    public void IncreaseManaBar(float mana)
    {
        float manaBar = GetManaBar();
        SetManaBar(manaBar + mana);
    }

    public void SetManaBar(float mana)
    {
        ManaBar.value = mana;
    }

    public float GetManaBar()
    {
        return ManaBar.value;
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
