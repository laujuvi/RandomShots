using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPila
{
    void InicializarPila(int maximo);

    // siempre que la pila este inicializada
    void Apilar(int x);
    // siempre que la pila este inicializada y no este vacıa
    void Desapilar();

    // siempre que la pila este inicializada
    bool PilaVacia();
    // siempre que la pila este inicializada y no este vacıa
    int Tope();

}
