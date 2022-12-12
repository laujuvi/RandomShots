using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{


    [SerializeField] private List<Gun> _gunPrefabs;
    [SerializeField] private Gun _gun;

    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject smg;
    [SerializeField] private GameObject shotgun;

    [SerializeField] private GameObject pistolOnPlayer;
    [SerializeField] private GameObject smgOnPlayer;
    [SerializeField] private GameObject shotgunOnPlayer;

    [SerializeField] private LayerMask _platformLayerMask;

    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _playerBody;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _maxStack;
    [SerializeField] private int _maxQueue;

    [SerializeField] private Bullet _defaultBullet;

    [SerializeField] public LifeController lifeController;

    /* COMMAND LIST */

    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    private TDAStack<Gun> _tdaStack;
    private TDAQueue<Bullet> _tdaQueue;
    private Collider2D _lastCollider;

    private bool isPlayerLeft = false;
    public bool IsPLayerLeft => isPlayerLeft;

    private void Start()
    {
        //ChangeWeapon(0);
        lifeController.InitializateLife();
        var mc = GetComponent<MovementController>();

        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

        _tdaStack = new TDAStack<Gun>(_maxStack);
        _tdaQueue = new TDAQueue<Bullet>(_maxQueue);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_lastCollider == collision) return;
        var queue = collision.gameObject.GetComponent<PickeableAmmo>();
        var stack = collision.gameObject.GetComponent<PickeableGun>();

        if (queue != null)
        {

            Debug.Log("Colision con queue Ammo");
            Debug.Log(PickUpQueue(queue.bulletType));


        }

        if (stack != null)
        {

            Debug.Log("Colision con stack Gun");
            Debug.Log(PickUpStack(stack.gunType));

         

            if (_gun == null)
            {
                ChangeWeapon();

            }
        }


        _lastCollider = collision;
        if (collision.gameObject.tag == "BulletEnemy")
        {
            BulletEnemyPower bulletEnemyPower = collision.gameObject.GetComponent<BulletEnemyPower>();
            lifeController.GetDamage(bulletEnemyPower.damage);
            Destroy(collision.gameObject);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (_lastCollider == collision.collider) return;
    //    var queue = collision.collider.gameObject.GetComponent<Ammo>();
    //    var stack = collision.collider.gameObject.GetComponent<PickeableGun>();

    //    if (queue != null)
    //    {

    //        Debug.Log("Colision con queue Ammo");
    //        PickUpQueue(queue);
    //        Debug.Log(_tdaQueue.First());

    //    }

    //    if (stack != null)
    //    {

    //        Debug.Log("Colision con queue Gun");
    //        Debug.Log(PickUpStack(stack.gunType));

    //    }

    //    _lastCollider = collision.collider;
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == _lastCollider) _lastCollider = null;
       

    }

    public void Attack()
    {
        if (_gun != null) GameManager.instance.AddEventQueue(_cmdAttack);
    }
    public void Reload() => _gun?.Reload();
    public void Jump()
    {
        Debug.Log("ENTER JUMP");

        GameManager.instance.AddEventQueue(_cmdMoveJump);

        //if (IsGrounded())
        //{
        //    GameManager.instance.AddEventQueue(_cmdMoveJump);
        //}
    }

    public void MoveLeft()
    {

        if (!isPlayerLeft)
        {
            PlayerFlip();
        }
        GameManager.instance.AddEventQueue(_cmdMoveLeft);



    }
    public void MoveRight()
    {

        if (isPlayerLeft)
        {
            PlayerFlip();
        }
        GameManager.instance.AddEventQueue(_cmdMoveRight);

    }

    public Gun PickUpStack(Gun gun)
    {
        _tdaStack.Stack(gun);



        return _tdaStack.Top();
    }

    public Bullet PickUpQueue(Bullet bullet)
    {
        _tdaQueue.Queue(bullet);
        return _tdaQueue.First();
    }

    public void ChangeWeapon()
    {
        
        var prefab = _tdaStack.Unstack();
        _gun = Instantiate<Gun>(prefab, _weaponTransform.position, _weaponTransform.rotation, transform);

        //PREGUNTO EL NOMBRE DEL PREFAB DEPENDIENDO ACTIVO IMAGENES EN PANTALLA
        if (prefab.name == "Pistol")
        {
            pistol.gameObject.SetActive(true);
            smg.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(false);
            pistolOnPlayer.gameObject.SetActive(true);
            smgOnPlayer.gameObject.SetActive(false);
            shotgunOnPlayer.gameObject.SetActive(false);

        }
        else if (prefab.name == "Smg")
        {
            pistol.gameObject.SetActive(false);
            smg.gameObject.SetActive(true);
            shotgun.gameObject.SetActive(false);
            pistolOnPlayer.gameObject.SetActive(false);
            smgOnPlayer.gameObject.SetActive(true);
            shotgunOnPlayer.gameObject.SetActive(false);
        }
        else if (prefab.name == "Shotgun")
        {
            pistol.gameObject.SetActive(false);
            smg.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(true); 
            pistolOnPlayer.gameObject.SetActive(false);
            smgOnPlayer.gameObject.SetActive(false);
            shotgunOnPlayer.gameObject.SetActive(true);
        }

        //---------------------------------------

        _gun.SetOwner(this);
        _gun.Reload();
        _gun.onEmptyAmmo += OutOfAmmo;
        _cmdAttack = new CmdAttack(_gun);

        if (_tdaQueue.Length <= 0)
        {
            _gun.BulletPrefab = _defaultBullet;
        } else
        {
            _gun.BulletPrefab = _tdaQueue.Dequeue();
        }

    }

    private void Update()
    {
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _tdaQueue.Dequeue();
            Debug.Log(_tdaQueue.Length);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _tdaStack.Unstack();
            Debug.Log(_tdaStack.Length);
        }

        //PlayerFlip();
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, _platformLayerMask);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(_boxCollider2D.bounds.center + new Vector3(_boxCollider2D.bounds.extents.x, 0), Vector2.down * (_boxCollider2D.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(_boxCollider2D.bounds.center - new Vector3(_boxCollider2D.bounds.extents.x, 0), Vector2.down * (_boxCollider2D.bounds.extents.y + 0.1f), rayColor);
        Debug.DrawRay(_boxCollider2D.bounds.center - new Vector3(_boxCollider2D.bounds.extents.x, _boxCollider2D.bounds.extents.y + 0.1f), Vector2.right * (_boxCollider2D.bounds.extents.x), rayColor);


        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void PlayerFlip()
    {

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isPlayerLeft = !isPlayerLeft;
    }

    private void OutOfAmmo()
    {
        if (_tdaStack.Length > 1) ChangeWeapon();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
    }

}
