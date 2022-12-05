using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickeableAmmo : MonoBehaviour
{
    [SerializeField] public Bullet bulletType;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("ENTRANDO AL TRIGGER");
        var colCharacter = collision.gameObject.GetComponent<Character>();

        if (colCharacter != null)
        {
            //Debug.Log("ES UN CHARACTER");

            Destroy(gameObject);
        }
    }
}
