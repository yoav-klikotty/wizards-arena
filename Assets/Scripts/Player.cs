using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int lifeAmount = 2;
    [SerializeField] TMP_Text LifeAmount;
    int ammoAmount = 0;
    [SerializeField] TMP_Text AmmoAmount;
    int healthBarAmmount = 5;
    [SerializeField] Slider HealthBar;

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
        int ammo = GetAmmo();
        SetAmmo(ammo + 1);
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

    public void ReduceHealthBar()
    {
        float healthBar = GetHealthBar();
        SetHealthBar((int)healthBar - 1);
    }

    public void SetHealthBar(int health)
    {
        HealthBar.value = health;
    }

    public float GetHealthBar()
    {
        return HealthBar.value;
    }
}
