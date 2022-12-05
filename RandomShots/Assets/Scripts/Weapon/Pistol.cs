using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{

    public override void Attack()
    {
        if (_currentBulletCount <= 0) return;

        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetOwner(this);

        base.Attack();
    }
}
