using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollitionDead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Character player = collision.gameObject.GetComponent<Character>();
            player.gameObject.SetActive(false);
        }
    }
}
