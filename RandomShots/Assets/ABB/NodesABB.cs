using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesABB : MonoBehaviour
{
    // datos a almacenar, en este caso un entero
    public int info;
    // referencia los nodos izquiero y derecho
    public NodesABB hijoIzq = null;
    public NodesABB hijoDer = null;
}
