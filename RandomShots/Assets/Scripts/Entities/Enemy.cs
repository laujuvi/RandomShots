using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int enemyLife ;
    [SerializeField] int maxLife = 100;
    public int MaxLife => maxLife;

    private void Start()
    {
        enemyLife = maxLife;
    }


    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        enemyLife -= damage;
        if (enemyLife <= 0) Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // var bullet = collision.gameObject.GetComponent<Bullet>();
        }
    }
}
