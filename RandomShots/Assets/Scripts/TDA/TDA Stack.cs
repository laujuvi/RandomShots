using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAStack<T> : IStack<T>
{
    T[] listStack; // arreglo en donde se guarda la informacion
    int index; // variable

    public int Length => index + 1;

    private Gun gun;
    private List<Gun> _gunPrefabs;

    public TDAStack(int max) {

        Init(max);

    }

    public void Stack(T x)
    {

        if (index + 1 >= listStack.Length) return;
        listStack[index] = x;
        index++;
    }

    public T Unstack()
    {
        if (index <= 0) return default;
        index--;
        return listStack[index];

    }

    public void Init(int max)
    {
        listStack = new T[max];
        index = 0;
    }

    public bool Clear()
    {
        return (index == 0);
    }

    public T Top()
    {
        return listStack[index - 1];
    }
    public T[] Print()
    {
        for (int i = index - 1; i >= 0; i--)
        {

            Debug.Log(listStack[i]);
        }

        return listStack;

    }

    public void ChangeWeapon(int index)
    {
        // Destroy(_gun?.gameObject);
        // DestroyImmediate(_gun?.gameObject, true);
        //gun = Instantiate(_gunPrefabs[index], transform);
        //gun.Reload();

    }
}

