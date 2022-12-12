using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHeal : Potions
{
    private void Awake()
    {
        id = 1;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Usar()
    {
        Debug.Log("cura");
    }
}

