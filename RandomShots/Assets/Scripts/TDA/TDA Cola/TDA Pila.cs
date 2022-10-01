using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAPila : IPila
{
    GameObject [] GunsStack; // arreglo en donde se guarda la informacion
    int indice; // variable

    [SerializeField] private Gun gun;
    [SerializeField] private List<Gun> _gunPrefabs;

    public void Apilar(GameObject x)
    {
        GunsStack[indice] = x;
        indice++;
    }

    public void Desapilar()
    {
        
        indice--;
        
    }

    public void InicializarPila(int Maximo)
    {
        GunsStack = new GameObject[Maximo];
        indice = 0;
    }

    public bool PilaVacia()
    {
        return (indice == 0);
    }

    public GameObject Tope()
    {
        return GunsStack[indice - 1];
    }
    public void imprimoPila()
    {
        for (int i = indice - 1; i >= 0; i--)
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

