using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private ActorStats _stats;

    public float Speed => _stats.MovementSpeed;

    public void Move(Vector3 direction) => transform.Translate(direction * Time.deltaTime * Speed);
}
