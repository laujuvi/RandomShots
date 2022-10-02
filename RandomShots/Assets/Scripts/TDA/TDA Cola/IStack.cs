using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack
{
    void Init(int maximo);

    // siempre que la pila este inicializada
    void Stack(GameObject x);
    // siempre que la pila este inicializada y no este vacıa
    void Unstack();

    // siempre que la pila este inicializada
    bool Clear();
    // siempre que la pila este inicializada y no este vacıa
    GameObject Top();

}
