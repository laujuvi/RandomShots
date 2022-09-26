using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Cola : ICola
{
    GameObject[] bullets;
    //int[] a; // arreglo en donde se guarda la informacion
    int indice; // variable
    public void Acolar(GameObject x)
    {
        for (int i = indice - 1; i >= 0; i--)
        {
            bullets[i + 1] = bullets[i];

        }

        bullets[0] = x;

        indice++;
    }
        public bool ColaVacia()
    {
        return (indice == 0);
    }

    public void Desacolar()
    {
        indice--;
    }

    public void Inicializarcola(int Maximo)
    {
        bullets = new GameObject[Maximo];
        indice = 0;
    }

    public GameObject Primero()
    {
        return bullets[indice-1];
    }
    public void ImprimoCola()
    {

        for (int i = indice ; i >= 0; i--)
        {
            Debug.Log(bullets[i].name);
        }
    }

}

