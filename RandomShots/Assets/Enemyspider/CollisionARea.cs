using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionARea : MonoBehaviour
{
    public int PropioNumero;
    public int numeroDeCollision;
    public LifeController lifeController;
    public EnemyBossSpider enemy;
    private void Start()
    {
        lifeController.InitializateLife();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemy.nodoselect = PropioNumero;
            numeroDeCollision = PropioNumero;
            lifeController.GetDamage(60);
            if (lifeController.currentLife <= 0)
            {
                enemy.lifeController.GetDamage(100);
                Destroy(collision.gameObject);
            }
        }
    }
}