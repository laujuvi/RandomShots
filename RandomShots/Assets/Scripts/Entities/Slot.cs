using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Dictionary<WeaponsPicked, GameObject> weaponReference = new Dictionary<WeaponsPicked, GameObject>();

    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _smg;
    [SerializeField] private GameObject _shotgun;

    private void Start()
    {

        weaponReference.Add(WeaponsPicked.Pistol, _pistol);
        weaponReference.Add(WeaponsPicked.Smg, _smg);
        weaponReference.Add(WeaponsPicked.Shotgun, _shotgun);

    }

    public void ShowWeapon(WeaponsPicked weapon) { 

        foreach (var pickedWeapon in weaponReference.Values)
        {
            pickedWeapon.SetActive(false);
        }

        weaponReference[weapon].SetActive(true);
    }

    public void DeactivateWeapon ()
    {
        foreach (var pickedWeapon in weaponReference.Values)
        {
            pickedWeapon.SetActive(false);
        }
    }
}
