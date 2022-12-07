using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smg : Gun
{
    public override void Attack()
    {
        if (_currentBulletCount <= 0) return;

        for (int i = 0; i < 3; i++)
        {
            Invoke(nameof(Shoot), i * .1f);
        }

        base.Attack();
    }

    private void Shoot()
    {
        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetOwner(this);
    }
}
