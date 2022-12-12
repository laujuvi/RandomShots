using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Character player;
    public float speed;
    public int vidaMaxima;
    public int manaMaximo;
    [SerializeField] public List<Transform> waypoints;
    public int nextPoint = 1;
    public float distance;
    //public Enemies hijoIzq = null;
    //public Enemies hijoDer = null;

    public Renderer render;
    public Color defaultColor;
    public Color newColor;
    private void Update()
    {
        MoveEnemy();
    }
    public void ChangeSpeed(float changeSpeed)
    {
        speed += changeSpeed;
    }
    public void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[nextPoint].position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, waypoints[nextPoint].transform.position) < distance)
        {
            nextPoint++;
            if (nextPoint >= waypoints.Count)
            {
                nextPoint = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.lifeController.GetDamage(20);
        }
    }

}
