using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TDAQueue : IQueue
{
    UnityEngine.GameObject[] bullets;
    //int[] a; // arreglo en donde se guarda la informacion
    int index; // variable
    public void Queue(UnityEngine.GameObject x)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            bullets[i + 1] = bullets[i];

        }

        bullets[0] = x;

        index++;
    }
        public bool Clear()
    {
        return (index == 0);
    }

    public void Dequeue()
    {
        index--;
    }

    public void Init(int Maximo)
    {
        bullets = new UnityEngine.GameObject[Maximo];
        index = 0;
    }

    public UnityEngine.GameObject First()
    {
        return bullets[index-1];
    }
    public void Print()
    {

        for (int i = index ; i >= 0; i--)
        {
            Debug.Log(bullets[i].name);
        }
    }

}

