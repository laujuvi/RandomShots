using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDAPila : IPila
{
    int [] GunsStack; // arreglo en donde se guarda la informacion
    int indice; // variable

    //[SerializeField] private Gun gun;
    [SerializeField] private List<Gun> _gunPrefabs;

    public void Apilar(int x)
    {
        Debug.Log("APILAR");
        GunsStack[indice] = x;
        indice++;
    }

    public void Desapilar()
    {
        Debug.Log("DESAPILAR");
        indice--;
        
    }

    public void InicializarPila(int Maximo)
    {
        Debug.Log("INICIALIZAR PILA");
        GunsStack = new int[Maximo];
        indice = 0;
    }

    public bool PilaVacia()
    {
        return (indice == 0);
    }

    public int Tope()
    {
        Debug.Log("TOPE "+ GunsStack[indice-1]);
        return GunsStack[indice - 1];
    }
    public void ImprimoPila()
    {
        for (int i = indice; i >= 0; i--)
        {

            Debug.Log(GunsStack[i]);
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

