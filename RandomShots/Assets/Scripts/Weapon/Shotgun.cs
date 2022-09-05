using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int _bulletCountPerShot = 5;

    public override void Attack()
    {
        if (_currentBulletCount <= 0) return;
        _currentBulletCount--;

        for (int i = 0; i < _bulletCountPerShot; i++)
        {
            var bullet = Instantiate(
                BulletPrefab,
                transform.position + Random.insideUnitSphere * 1,
                Quaternion.identity);
            bullet.GetComponent<Bullet>().SetOwner(this);
        }
    }
}
