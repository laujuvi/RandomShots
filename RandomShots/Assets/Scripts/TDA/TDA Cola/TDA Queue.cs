using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TDAQueue : IQueue
{
    UnityEngine.GameObject[] bullets;
    //int[] a; // arreglo en donde se guarda la informacion
    int indice; // variable
    public void Queue(UnityEngine.GameObject x)
    {
        for (int i = indice - 1; i >= 0; i--)
        {
            bullets[i + 1] = bullets[i];

        }

        bullets[0] = x;

        indice++;
    }
        public bool Clear()
    {
        return (indice == 0);
    }

    public void Dequeue()
    {
        indice--;
    }

    public void Init(int Maximo)
    {
        bullets = new UnityEngine.GameObject[Maximo];
        indice = 0;
    }

    public UnityEngine.GameObject First()
    {
        return bullets[indice-1];
    }
    public void Print()
    {

        for (int i = indice ; i >= 0; i--)
        {
            Debug.Log(bullets[i].name);
        }
    }

}

