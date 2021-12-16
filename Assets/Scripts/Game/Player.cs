using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Slider _manaBar;

    [SerializeField] Item _wand;
    [SerializeField] Item _cape;
    [SerializeField] Item _hat;

    public void ReduceHealthBar(float damage)
    {
        float healthBar = GetHealthBar();
        SetHealthBar(healthBar - damage);
    }

    public void SetHealthBar(float health)
    {
        _healthBar.value = health;
    }

    public float GetHealthBar()
    {
        return _healthBar.value;
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
        _manaBar.value = mana;
    }

    public float GetManaBar()
    {
        return _manaBar.value;
    }

    public void setWand(Item wand){
        this._wand = wand;
    }
    public Item getWand(){
        return this._wand;
    }

    public void setCape(Item cape){
        this._cape = cape;
    }
    public Item getCape(){
        return this._cape;
    }
    public void setHat(Item hat){
        this._hat = hat;
    }
    public Item getHat(){
        return this._hat;
    }
}
