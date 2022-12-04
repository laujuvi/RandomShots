using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TDAQueue<T> : IQueue<T>
{
    T[] list;
    //int[] a; // arreglo en donde se guarda la informacion
    int index; // variable
    public T[] List => list;

    public int Length => index + 1;

    public TDAQueue(int max)
    {
        Init(max);
    }
    public void Queue(T x)
    {
        if (index + 1 >= list.Length) return;

        for (int i = index; i >= 0; i--)
        {
            list[i + 1] = list[i];
        }

        list[0] = x;

        index++;
    }
    public bool Clear()
    {
        return (index == 0);
    }

    public void Dequeue()
    {
        if (index < 0) return;
        index--;
    }

    public void Init(int max)
    {
        list = new T[max];
        index = 0;
    }

    public T First()
    {
        if (index - 1 < 0) return default;

        return list[index - 1];
    }
    public void Print()
    {

        for (int i = index; i >= 0; i--)
        {
            Debug.Log(list[i]);
        }
    }

}

