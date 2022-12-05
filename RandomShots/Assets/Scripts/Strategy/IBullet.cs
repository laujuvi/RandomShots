using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    IGun Owner { get; }
    float Speed { get; }
    int Damage { get; }
    float LifeTime { get; }

    void Travel();
    void OnTriggerEnter2D(Collider2D collider);
    void SetOwner(IGun gun);
}
