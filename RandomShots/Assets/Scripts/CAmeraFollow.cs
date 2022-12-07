using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAmeraFollow : MonoBehaviour
{

    public GameObject player;
    public Camera camera;

    private void Update()
    {
        camera.gameObject.transform.position= new Vector3(player.transform.position.x, player.transform.position.y+2, -10);
    }
}
