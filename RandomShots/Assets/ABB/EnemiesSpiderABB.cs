using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpiderABB : MonoBehaviour
{
    public int enemieSelect;
    ABB arbol = new ABB();

    // Start is called before the first frame update
    [SerializeField]  private Enemies[] enemiesArrey;
    [HideInInspector] public List<EnemyNode> enemyNodesListInOrder = new List<EnemyNode>();
    [HideInInspector] public List<EnemyNode> enemyListPreOrder = new List<EnemyNode>();
    private void Awake()
    {
    }
    private void Start()
    {
        Arbol();
    }
    void Arbol()
    {

        arbol.InicializarArbol();

        // agrego los mismos elementos del vector al arbol
        for (int i = 0; i < enemiesArrey.Length; i++)
        {
            arbol.AgregarElem(ref arbol.enemyRaiz, enemiesArrey[i]);
        }

        // In-Order
        inOrder(arbol.enemyRaiz);
        float speed = 0;
        for (int i = 0; i < enemyNodesListInOrder.Count; i++)
        {
            //cambiar color gradualmente
            enemyNodesListInOrder[i].enemieInstance.ChangeSpeed(speed);
            speed += 2;
        }
    }

    int altura(EnemyNode ab)
    {
        if (ab == null)
        {
            return -1;
        }
        else
        {
            return (1 + Math.Max(altura(ab.hijoIzq), altura(ab.hijoDer)));
        }
    }

    void preOrder_FE(EnemyNode a)
    {
        if (a != null)
        {
            // accion mientras recorro //
            Console.WriteLine("Nodo Padre: " + a.vidaMaxima.ToString());
            Console.WriteLine("Altura Izquierda: " + altura(a.hijoDer));
            Console.WriteLine("Altura Derecha: " + altura(a.hijoIzq));
            Console.WriteLine();
            //                         //

            preOrder_FE(a.hijoIzq);
            preOrder_FE(a.hijoDer);
        }
    }

    void preOrder(EnemyNode a)
    {
        if (a != null)
        {
            Console.WriteLine(a.vidaMaxima.ToString());
            enemyListPreOrder.Add(a);
            preOrder(a.hijoIzq);
            preOrder(a.hijoDer);
        }
    }

    void inOrder(EnemyNode a)
    {
        if (a != null)
        {
            inOrder(a.hijoIzq);
            enemyNodesListInOrder.Add(a);
            //Debug.Log("VidaMAxima " + a.enemieInstance.vidaMaxima.ToString() + " ManaMaximo " + a.enemieInstance.manaMaximo.ToString());
            inOrder(a.hijoDer);
        }
    }

    void postOrder(EnemyNode a)
    {
        if (a != null)
        {
            postOrder(a.hijoIzq);
            postOrder(a.hijoDer);
            Console.WriteLine(a.vidaMaxima.ToString());
        }
    }

    void level_Order(EnemyNode nodo)
    {
        Queue<EnemyNode> q = new Queue<EnemyNode>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();

            Console.WriteLine(nodo.vidaMaxima.ToString());

            if (nodo.hijoIzq != null) { q.Enqueue(nodo.hijoIzq); }

            if (nodo.hijoDer != null) { q.Enqueue(nodo.hijoDer); }
        }
    }

    void levelOrder(EnemyNode nodo)

    {
        Queue<EnemyNode> q = new Queue<EnemyNode>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();

            Console.WriteLine("Padre: " + nodo.vidaMaxima.ToString());

            if (nodo.hijoIzq != null)
            {
                q.Enqueue(nodo.hijoIzq);
                Console.WriteLine("Hijo Izq: " + nodo.hijoIzq.vidaMaxima.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Izq: null");
            }

            if (nodo.hijoDer != null)
            {
                q.Enqueue(nodo.hijoDer);
                Console.WriteLine("Hijo Der: " + nodo.hijoDer.vidaMaxima.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Der: null");
            }
        }
    }
    public void DestroyEnemies(int numeroEnemigoMuerto)
    {
        int position = (numeroEnemigoMuerto / 10) - 1;

        arbol.EliminarElem(ref arbol.enemyRaiz, enemiesArrey[position]);

        enemyNodesListInOrder[position].enemieInstance.gameObject.SetActive(false);
    }
}

public interface ABBTDA
{
    int Raiz();

    EnemyNode HijoIzq();

    EnemyNode HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(ref EnemyNode n, Enemies enemy);
    void EliminarElem(ref EnemyNode n, Enemies enemy);
}