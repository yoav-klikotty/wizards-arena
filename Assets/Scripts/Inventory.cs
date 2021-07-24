using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Weapon[] weapons;
    [SerializeField] int currentWeaponIndex;
    [SerializeField] int maxWeapons;
    public Weapon GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }

    public void UpgardeWeapon()
    {
        if (currentWeaponIndex < maxWeapons - 1)
        {
            currentWeaponIndex = 1 + currentWeaponIndex;
        }
    }
}
