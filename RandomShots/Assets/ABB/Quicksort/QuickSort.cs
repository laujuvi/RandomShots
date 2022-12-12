using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    public Potions[] potionsArrey;
    public List<Potions> potionsOrder = new List<Potions>();

    private void Awake()
    {
        quickSort(potionsArrey, 0, potionsArrey.Length - 1); 
    }
    private void Start()
    {
        imprimirVector(potionsArrey);
    }

    static public int Partition(Potions[] arr, int left, int right)
    {
        int pivot;

        int aux = (left + right) / 2;   //tomo el valor central del vector
        pivot = arr[aux].id;


        // en este ciclo debo dejar todos los valores menores al pivot
        // a la izquierda y los mayores a la derecha
        while (true)
        {

            while (arr[left].id < pivot)
            {
                left++;
            }
            while (arr[right].id > pivot)
            {
                right--;
            }
            if (left < right)
            {
                Potions temp = arr[right];
                arr[right] = arr[left];
                arr[left] = temp;
            }
            else
            {
                // este es el valor que devuelvo como proxima posicion de
                // la particion en el siguiente paso del algoritmo
                return right;
            }
        }

    }
    public void quickSort(Potions[] arr, int left, int right)
    {
        int pivot;

        if (left < right)
        {
            pivot = Partition(arr, left, right);

            if (pivot > 1)
            {
                // mitad del lado izquierdo del vector
                quickSort(arr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {

                // mitad del lado derecho del vector
                quickSort(arr, pivot + 1, right);
            }
        }
    }

    public void imprimirVector(Potions[] vec)
    {

        for (int i = 0; i < vec.Length; i++)
        {
            potionsOrder.Add(vec[i]);
        }
    }
}



