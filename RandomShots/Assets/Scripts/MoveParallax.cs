using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParallax : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float parallaxmultiplador;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float spriteWidth, startPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;

    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        float moveAmount = cameraTransform.position.x * (1 - parallaxmultiplador);
        transform.position += new Vector3(deltaMovement.x * parallaxmultiplador, 0f, 0f);
        lastCameraPosition = cameraTransform.position;
        if (moveAmount < startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;

        }
        else if (moveAmount < startPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            startPosition -= spriteWidth;
        }

    }
}
