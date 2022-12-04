using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack<T>
{
    void Init(int max);

    // siempre que la pila este inicializada
    void Stack(T x);
    // siempre que la pila este inicializada y no este vacıa
    T Unstack();

    // siempre que la pila este inicializada
    bool Clear();
    // siempre que la pila este inicializada y no este vacıa
    T Top();

}
