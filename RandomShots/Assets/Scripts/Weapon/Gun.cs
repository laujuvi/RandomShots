using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    public GunStats Stats => _stats;
    [SerializeField] protected GunStats _stats;

    public GameObject BulletPrefab => _stats.BulletPrefab;
    public int MagSize => _stats.MagSize;
    protected int _currentBulletCount;
    public int Damage => _stats.BulletDamage;

    public virtual void Attack()
    {
        if (_currentBulletCount <= 0) return;
        _currentBulletCount--;

        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetOwner(this);
    }

    public virtual void Reload() => _currentBulletCount = MagSize;
}
