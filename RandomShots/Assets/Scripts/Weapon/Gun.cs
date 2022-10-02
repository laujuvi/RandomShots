using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    public GunStats Stats => _stats;
    [SerializeField] protected GunStats _stats;

    private Character owner;

    public GameObject BulletPrefab => _stats.BulletPrefab;
    public int MagSize => _stats.MagSize;
    protected int _currentBulletCount;
    public int Damage => _stats.BulletDamage;

    public bool IsPlayerLeft => owner.IsPLayerLeft;
    public virtual void Attack()
    {
        if (_currentBulletCount <= 0) return;
        _currentBulletCount--;

        Debug.Log(transform.transform.localScale.x);

        var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetOwner(this);
    }

    public void SetOwner(Character character)
    {
        owner = character;
    }

    public virtual void Reload() => _currentBulletCount = MagSize;
}
