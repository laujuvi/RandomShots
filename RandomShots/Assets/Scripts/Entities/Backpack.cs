using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;

    private void AddWeapon (WeaponsPicked weapon, int index) {
        slots[index].ShowWeapon(weapon);
    }

    public void StackWeapons (TDAStack<WeaponsPicked> weapons)
      {
        var weaponList = weapons.Print();
        Array.Reverse(weaponList);
        for (int i = 0; i < slots.Count; i++)
        {
            if (weaponList.Length > i) AddWeapon(weaponList[i], i); else RemoveWeapon(i);
        }

    }

    private void RemoveWeapon (int index)
    {
        slots[index].DeactivateWeapon();
    }

}
