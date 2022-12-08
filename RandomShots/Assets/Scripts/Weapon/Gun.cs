using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{

    public GunStats Stats => _stats;
    [SerializeField] protected GunStats _stats;
    //[SerializeField] Quaternion angleBullet;

    public Action onEmptyAmmo = delegate { };

    private Character owner;

    public Bullet BulletPrefab { get; set; }

    public int MagSize => _stats.MagSize;
    protected int _currentBulletCount;
    public int Damage => _stats.BulletDamage;

    public bool IsPlayerLeft => owner.IsPLayerLeft;
    public virtual void Attack()
    {
        _currentBulletCount--;

        //if (_currentBulletCount <= 0) return;
        if (_currentBulletCount <= 0)
        {
            onEmptyAmmo.Invoke();
            Debug.Log("OUT OF AMMO!");
            // Destroy(gameObject);
        }


        //Debug.Log(transform.transform.localScale.x);

        // var bullet = Instantiate(BulletPrefab, transform.position, angleBullet);
        // bullet.GetComponent<Bullet>().SetOwner(this);
    }

    public void SetOwner(Character character)
    {
        owner = character;
    }

    public virtual void Reload() => _currentBulletCount = MagSize;
}
