using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    [SerializeField] private List<int> _layerMasks;

    public IGun Owner => _owner;
    private IGun _owner;

    public float Speed => Owner.Stats.BulletSpeed;
    public int Damage => Owner.Stats.BulletDamage;
    public float LifeTime => Owner.Stats.BulletLifeTime;
    private float _travelDirection;
    private float _currentLifeTime;

    //public void Travel() => transform.position += Vector3.forward * Time.deltaTime * Speed;

    //public void Travel() => transform.position += Vector3.right * Time.deltaTime * Speed;

    public void Travel() {
        //Debug.Log(transform.rotation);
        transform.position += Vector3.right * Time.deltaTime * Speed * _travelDirection;
    } 

    public void OnTriggerEnter(Collider collider)
    {
        if (_layerMasks.Contains(collider.gameObject.layer))
        {
            IDamageable lifeController = collider.gameObject.GetComponent<IDamageable>();
            GameManager.instance.AddEventQueue(new CmdDoDamage(lifeController, Damage));

            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Debug.Log(_owner.Stats.BulletSpeed);
        Debug.Log(_owner.Stats.BulletLifeTime);
        Debug.Log(_owner.Stats.BulletDamage);


        _currentLifeTime = LifeTime;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _travelDirection = _owner.IsPlayerLeft ? -1 : 1;
    }

    void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0) Destroy(this.gameObject);

        Travel();
    }

    public void SetOwner(IGun gun) => _owner = gun;
}
