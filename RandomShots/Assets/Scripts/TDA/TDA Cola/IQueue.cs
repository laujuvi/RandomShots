using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


interface IQueue
{
    void Queue(UnityEngine.GameObject x);
    void Dequeue();
    UnityEngine.GameObject First();
    bool Clear();
    void Init(int maximo);
}

//vAcolar: permite agregar un elemento a la estructura. Se supone que la cola está inicializada.

//vDesacolar: permite eliminar el primer elemento agregado a la estructura. Se supone como precondición que la cola no esté vacía.

//vPrimero: permite conocer cuál es el primer elemento ingresado a la estructura. Se supone que la cola no está vacía.

//vColaVacía: indica si la cola contiene elementos o no. Se supone que la cola está inicializada.

//vInicializarCola: permite inicializar la estructura de la cola.