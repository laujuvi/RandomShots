using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickeableGun : MonoBehaviour
{
    [SerializeField] public WeaponsPicked gunType;
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
