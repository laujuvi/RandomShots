using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


interface IQueue<T>
{
    void Queue(T x);
    void Dequeue();
    T First();
    bool Clear();
    void Init(int maximo);
}

//vAcolar: permite agregar un elemento a la estructura. Se supone que la cola esta inicializada.

//vDesacolar: permite eliminar el primer elemento agregado a la estructura. Se supone como precondiccion que la cola no este vacia.

//vPrimero: permite conocer cual es el primer elemento ingresado a la estructura. Se supone que la cola no esta vacia.

//vColaVacia: indica si la cola contiene elementos o no. Se supone que la cola esta inicializada.

//vInicializarCola: permite inicializar la estructura de la cola.