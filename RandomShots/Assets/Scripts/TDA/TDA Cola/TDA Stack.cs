using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAStack : IStack
{
    GameObject [] GunsStack; // arreglo en donde se guarda la informacion
    int index; // variable

    [SerializeField] private Gun gun;
    [SerializeField] private List<Gun> _gunPrefabs;

    public void Stack(GameObject x)
    {
        GunsStack[index] = x;
        index++;
    }

    public void Unstack()
    {
        
        index--;
        
    }

    public void Init(int Maximo)
    {
        GunsStack = new GameObject[Maximo];
        index = 0;
    }

    public bool Clear()
    {
        return (index == 0);
    }

    public GameObject Top()
    {
        return GunsStack[index - 1];
    }
    public void Print()
    {
        for (int i = index - 1; i >= 0; i--)
        {
            Debug.Log(GunsStack[i].name);
        }
    }

    public void ChangeWeapon(int index)
    {
        // Destroy(_gun?.gameObject);
        // DestroyImmediate(_gun?.gameObject, true);
        //gun = Instantiate(_gunPrefabs[index], transform);
        //gun.Reload();
        
    }
}

