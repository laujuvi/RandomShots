using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTemp : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    
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
    }
}
