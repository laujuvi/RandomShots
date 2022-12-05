using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyPower : MonoBehaviour
{
    private float currentTime;
    private float timeToDeath=5f;
    private float speed = 5f;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime < timeToDeath)
        {
            transform.position += transform.right * Time.deltaTime * speed;
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }
}
