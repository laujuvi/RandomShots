using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode
{
    public int vidaMaxima;
    public EnemyNode hijoDer;
    public EnemyNode hijoIzq;
    public Enemies enemieInstance;
}
public class ABB : ABBTDA
{
    //public NodesABB raiz;
    //public Enemies enemyRaiz;
    public EnemyNode enemyRaiz;

    public int Raiz()
    {
        return enemyRaiz.enemieInstance.vidaMaxima;
    }

    public bool ArbolVacio()
    {
        return (enemyRaiz == null);
    }

    public void InicializarArbol()
    {
        enemyRaiz = null;
    }

    public EnemyNode HijoDer()
    {
        return enemyRaiz.hijoDer;
    }

    public EnemyNode HijoIzq()
    {
        return enemyRaiz.hijoIzq;
    }

    public void AgregarElem(ref EnemyNode enemyRaiz, Enemies enemy)
    {
        if (enemyRaiz == null)
        {
            enemyRaiz = new EnemyNode();
            enemyRaiz.enemieInstance = enemy;
        }
        else if (enemyRaiz.enemieInstance.vidaMaxima > enemy.vidaMaxima)
        {
            AgregarElem(ref enemyRaiz.hijoIzq, enemy);
        }
        else if (enemyRaiz.enemieInstance.vidaMaxima < enemy.vidaMaxima)
        {
            AgregarElem(ref enemyRaiz.hijoDer , enemy);
        }
    }

    public void EliminarElem(ref EnemyNode enemyRaiz, Enemies enemy)
    {
        if (enemyRaiz != null)
        {
            if (enemyRaiz.vidaMaxima == enemyRaiz.enemieInstance.vidaMaxima && (enemyRaiz.hijoIzq == null) && (enemyRaiz.hijoDer == null))
            {
                enemyRaiz = null;
            }
            else if (enemyRaiz.vidaMaxima == enemyRaiz.enemieInstance.vidaMaxima && enemyRaiz.hijoIzq != null)
            {
                enemyRaiz.vidaMaxima = this.mayor(enemyRaiz.hijoIzq);
                EliminarElem(ref enemyRaiz.hijoIzq, enemy);
            }
            else if (enemyRaiz.vidaMaxima == enemyRaiz.enemieInstance.vidaMaxima && enemyRaiz.hijoIzq == null)
            {
                enemyRaiz.vidaMaxima = this.menor(enemyRaiz.hijoDer);
                EliminarElem(ref enemyRaiz.hijoDer, enemy);
            }
            else if (enemyRaiz.vidaMaxima < enemyRaiz.enemieInstance.vidaMaxima)
            {
                EliminarElem(ref enemyRaiz.hijoDer,enemy);
            }
            else
            {
                EliminarElem(ref enemyRaiz.hijoIzq, enemy);
            }
        }
    }

    public int mayor(EnemyNode a)
    {
        if (a.hijoDer == null)
        {
            return a.enemieInstance.vidaMaxima;
        }
        else
        {
            return mayor(a.hijoDer);
        }
    }

    public int menor(EnemyNode a)
    {
        if (a.hijoIzq == null)
        {
            return a.enemieInstance.vidaMaxima;
        }
        else
        {
            return menor(a.hijoIzq);
        }

    }

}

