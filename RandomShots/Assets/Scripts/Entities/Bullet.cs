using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    #region PRIVATE_PROPERTIES
    private Rigidbody _rigidbody;
    private Collider _collider;
    [SerializeField] private List<int> _layerMasks;
    #endregion

    #region I_BULLET_PROPERTIES
    public IGun Owner => _owner;
    private IGun _owner;

    public float Speed => Owner.Stats.BulletSpeed;
    public int Damage => Owner.Stats.BulletDamage;
    public float LifeTime => Owner.Stats.BulletLifeTime;
    private float _currentLifeTime;
    #endregion

    #region I_BULLET_METHODS
    public void Travel() => transform.position += Vector3.forward * Time.deltaTime * Speed;

    public void OnTriggerEnter(Collider collider)
    {
        if (_layerMasks.Contains(collider.gameObject.layer))
        {
            IDamageable lifeController = collider.gameObject.GetComponent<IDamageable>();
            GameManager.instance.AddEventQueue(new CmdDoDamage(lifeController, Damage));

            Destroy(this.gameObject);
        }
    }
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        _currentLifeTime = LifeTime;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    void Update()
    {
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0) Destroy(this.gameObject);

        Travel();
    }
    #endregion

    public void SetOwner(IGun gun) => _owner = gun;
}
