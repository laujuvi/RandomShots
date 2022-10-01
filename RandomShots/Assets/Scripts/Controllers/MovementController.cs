using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private ActorStats _stats;
    //[SerializeField] private float _jumpForce;

    //[SerializeField] private Rigidbody2D _playerBody;


    public float Speed => _stats.MovementSpeed;
    
    //public float jumpForce => _stats.JumpForce;

    public void Move(Vector3 direction) => transform.Translate(direction * Time.deltaTime * Speed);

    public void Jump(Rigidbody2D playerBody, float jumpForce, Vector2 direction) => playerBody.AddForce(direction * jumpForce, ForceMode2D.Impulse);

}
