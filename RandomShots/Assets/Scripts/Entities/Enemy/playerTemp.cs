using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTemp :MonoBehaviour
{
    public float speed;
    private Cola cola;
    public int maximo;
    public int currentMax;
    private void Awake()
    {
        cola = new Cola();
    }
    void Start()
    {
        cola.Inicializarcola(maximo);
    }

    
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

       Vector2 direction = new Vector3(x, v);
        direction.Normalize();

        if (x != 0f || v != 0f)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

      
         if (Input.GetKeyDown(KeyCode.P))
         { 
            cola.Desacolar();
            cola.ImprimoCola();
         }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullets"))
        {
            
            if (currentMax < maximo)
            {
                cola.Acolar(collision.gameObject);
                currentMax++;
                collision.gameObject.SetActive(false);
            }

        }

    }
}
