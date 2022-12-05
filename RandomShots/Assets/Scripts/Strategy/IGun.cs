using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun : IWeapon
{
    GunStats Stats { get; }
    Bullet BulletPrefab { get; }
    int MagSize { get; }
    bool IsPlayerLeft { get; }

    void Reload();
}

