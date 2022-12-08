using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WeaponsPicked {Empty ,Pistol, Shotgun, Smg};

public class Character : Actor
{
    Dictionary<WeaponsPicked, CmdAttack> weaponCommand = new Dictionary<WeaponsPicked, CmdAttack>();
    Dictionary<WeaponsPicked, Gun> weaponReference = new Dictionary<WeaponsPicked, Gun>();


    [SerializeField] private Pistol _pistolPrefab;
    [SerializeField] private Shotgun _shotgunPrefab;
    [SerializeField] private Smg _smgPrefab;

    [SerializeField] private Gun _gun;


    [SerializeField] private LayerMask _platformLayerMask;

    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _playerBody;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _maxStack;
    [SerializeField] private int _maxQueue;

    [SerializeField] private Bullet _defaultBullet;

    [SerializeField] private LifeController lifeController;

    /* COMMAND LIST */

    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    private TDAStack<WeaponsPicked> _tdaStack;
    private TDAQueue<Bullet> _tdaQueue;
    private Collider2D _lastCollider;

    private bool isPlayerLeft = false;
    public bool IsPLayerLeft => isPlayerLeft;

    private void Start()
    {

        var pistol = Instantiate<Gun>(_pistolPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);
        var shotgun = Instantiate<Gun>(_shotgunPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);
        var smg = Instantiate<Gun>(_smgPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);

        lifeController.InitializateLife();
        var mc = GetComponent<MovementController>();

        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

        _tdaStack = new TDAStack<WeaponsPicked>(_maxStack);
        _tdaQueue = new TDAQueue<Bullet>(_maxQueue);

        weaponReference.Add(WeaponsPicked.Pistol, pistol);
        weaponReference.Add(WeaponsPicked.Shotgun, shotgun);
        weaponReference.Add(WeaponsPicked.Smg, smg);

        weaponCommand.Add(WeaponsPicked.Pistol, new CmdAttack(pistol));
        weaponCommand.Add(WeaponsPicked.Shotgun, new CmdAttack(shotgun));
        weaponCommand.Add(WeaponsPicked.Smg, new CmdAttack(smg));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_lastCollider == collision) return;
        var queue = collision.gameObject.GetComponent<PickeableAmmo>();
        var stack = collision.gameObject.GetComponent<PickeableGun>();

        if (queue != null)
        {

            PickUpQueue(queue.bulletType);

        }

        if (stack != null)
        {

            PickUpStack(stack.gunType);
            if (_gun == null) ChangeWeapon();

        }

        _lastCollider = collision;
        if (collision.gameObject.tag == "BulletEnemy")
        {
            BulletEnemyPower bulletEnemyPower = collision.gameObject.GetComponent<BulletEnemyPower>();
            lifeController.GetDamage(bulletEnemyPower.damage);
            Destroy(collision.gameObject);
        }
    }
    
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

    public WeaponsPicked PickUpStack(WeaponsPicked weapon)
    {
        _tdaStack.Stack(weapon);

        return _tdaStack.Top();
    }

    public Bullet PickUpQueue(Bullet bullet)
    {
        _tdaQueue.Queue(bullet);
        return _tdaQueue.First();
    }

    public void ChangeWeapon()
    {
        var unstackedWeapon = _tdaStack.Unstack();
        Debug.Log($"Changing weapon to {unstackedWeapon}");
        

        if (unstackedWeapon == WeaponsPicked.Empty)
        {
            _cmdAttack = null;
            return;
        }

        // desuscribe  las armas
        foreach (var gun in weaponReference.Values)
        {
            gun.onEmptyAmmo = delegate { };
        }

        _gun = weaponReference[unstackedWeapon];
        _gun.SetOwner(this);
        _gun.Reload();

        _gun.onEmptyAmmo += OutOfAmmo;
        _cmdAttack = weaponCommand[unstackedWeapon];

        CheckBullestGun(_gun);

    }

    [SerializeField] private TDAStack<int> numbers = new TDAStack<int>(100);
    [SerializeField] private int count = 0;
    private void Update()
    {
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            numbers.Stack(count);
            count++;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(numbers.Unstack());
            Debug.Log($"Remaining numbers in stack {numbers.Length}");
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

    private void CheckBullestGun (Gun gun){
        if (_tdaQueue.Length <= 0)
        {
            gun.BulletPrefab = _defaultBullet;
        }
        else
        {
            gun.BulletPrefab = _tdaQueue.Dequeue();
        }

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
        _gun = null;
        Debug.Log($"Remaining weapons in stack: {_tdaStack.Length}");
        if (_tdaStack.Length > 0) ChangeWeapon();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
    }

}
