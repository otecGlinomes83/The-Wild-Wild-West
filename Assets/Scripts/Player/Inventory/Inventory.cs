using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Weapon> _weapons;
    private List<Weapon> _currentWeapons;

    public void Setup(List<Weapon> weapons)
    {
        _weapons = weapons;
        _currentWeapons = weapons.ToList();
    }

    public Weapon GetCurrentWeapon()
    {
        Weapon currentWeapon = _currentWeapons[0];
        _currentWeapons.Remove(currentWeapon);
        currentWeapon.gameObject.SetActive(true);

        return currentWeapon;
    }

    public void ReturnWeapon(Weapon weapon)
    {
        if (_currentWeapons.Contains(weapon))
            return;

        weapon.gameObject.SetActive(false);
        _currentWeapons.Add(weapon);
    }
}
