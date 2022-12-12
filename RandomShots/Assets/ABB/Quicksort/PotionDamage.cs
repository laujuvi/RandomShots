using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDamage : Potions
{
    private void Awake()
    {
        id = 2;

    }
    public void Usar()
    {
        Debug.Log("mas daño");

    }

}
