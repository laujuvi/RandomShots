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
    void OnTriggerEnter(Collider collider);
    void SetOwner(IGun gun);
}
