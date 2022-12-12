using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpeed : Potions
{
    private void Awake()
    {
        id = 3;
    }
    public void Usar()
    {
        Debug.Log("mas velocidad");

    }

}
