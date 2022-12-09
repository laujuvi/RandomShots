using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WeaponsPicked {Empty ,Pistol, Shotgun, Smg};

public class Character : Actor
{
    //DICCIONARIOS
    Dictionary<WeaponsPicked, CmdAttack> weaponCommand = new Dictionary<WeaponsPicked, CmdAttack>();
    Dictionary<WeaponsPicked, Gun> weaponReference = new Dictionary<WeaponsPicked, Gun>();

    //PREFABS DE ARMAS
    [SerializeField] private Pistol _pistolPrefab;
    [SerializeField] private Shotgun _shotgunPrefab;
    [SerializeField] private Smg _smgPrefab;
    [SerializeField] private Gun _gun;

    //PREFABS BULLETS
    [SerializeField] private Bullet _defaultBullet;

    //LEYERS
    [SerializeField] private LayerMask _platformLayerMask;

    //PLAYER
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _playerBody;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private LifeController lifeController;
    private Collider2D _lastCollider;
    private bool isPlayerLeft = false;
    public bool IsPLayerLeft => isPlayerLeft;

    //TDA
    [SerializeField] private int _maxStack;
    [SerializeField] private int _maxQueue;
    private TDAStack<WeaponsPicked> _tdaStack;
    private TDAQueue<Bullet> _tdaQueue;

    // COMMAND LIST
    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    /*START*/

    private void Start()
    {
        // INSTANCIACION DE LOS PREFABS
        var pistol = Instantiate<Gun>(_pistolPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);
        var shotgun = Instantiate<Gun>(_shotgunPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);
        var smg = Instantiate<Gun>(_smgPrefab, _weaponTransform.position, _weaponTransform.rotation, transform);

        // LIFE CONTROLLER
        lifeController.InitializateLife();

        // COMMANDS
        var mc = GetComponent<MovementController>();
        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

        //TDA
        _tdaStack = new TDAStack<WeaponsPicked>(_maxStack);
        _tdaQueue = new TDAQueue<Bullet>(_maxQueue);

        // AGREGADO DATOS A LOS DICCIONARIOS
        weaponReference.Add(WeaponsPicked.Pistol, pistol);
        weaponReference.Add(WeaponsPicked.Shotgun, shotgun);
        weaponReference.Add(WeaponsPicked.Smg, smg);

        weaponCommand.Add(WeaponsPicked.Pistol, new CmdAttack(pistol));
        weaponCommand.Add(WeaponsPicked.Shotgun, new CmdAttack(shotgun));
        weaponCommand.Add(WeaponsPicked.Smg, new CmdAttack(smg));
    }

    /*COLISIONES*/

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
    
    //CUANDO NO COLISIONO EL COLLIDER QUEDA EN NULL
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == _lastCollider) _lastCollider = null;
    }


    /*ACCIONES DEL PLAYER*/

    public void Attack()
    {
        if (_gun != null) GameManager.instance.AddEventQueue(_cmdAttack);
    }
    public void Reload() => _gun?.Reload();
    public void Jump()=> GameManager.instance.AddEventQueue(_cmdMoveJump);

    public void MoveLeft()
    {
        if (!isPlayerLeft)
        {
            PlayerFlip();
        }
        GameManager.instance.AddEventQueue(_cmdMoveLeft);

    }

    /*ACCIONES DEL TDA*/

    // STEACKEA LAS ARMAS PICKEADAS
    public WeaponsPicked PickUpStack(WeaponsPicked weapon)
    {
        _tdaStack.Stack(weapon);

        return _tdaStack.Top();
    }

    //HACE UN QUEUE DE LAS BALLAS PICKEADAS
    public Bullet PickUpQueue(Bullet bullet)
    {
        _tdaQueue.Queue(bullet);
        return _tdaQueue.First();
    }

    /*FUNCIONES DE LAS ARMAS*/
    public void ChangeWeapon()
    {
        var unstackedWeapon = _tdaStack.Unstack();

        //VERIFICA SI HAY ARMAS EN EL STACK
        if (unstackedWeapon == WeaponsPicked.Empty)
        {
            _cmdAttack = null;
            return;
        }

        // DESUSCRIBE EL EVENTO
        foreach (var gun in weaponReference.Values)
        {
            gun.onEmptyAmmo = delegate { };
        }

        _gun = weaponReference[unstackedWeapon];
        _gun.SetOwner(this);
        _gun.Reload();

        // SUSCRIBE EL EVENTO
        _gun.onEmptyAmmo += OutOfAmmo;

        _cmdAttack = weaponCommand[unstackedWeapon];

        CheckBullestGun(_gun);

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
    private void OutOfAmmo()
    {
        _gun = null;
        Debug.Log($"Remaining weapons in stack: {_tdaStack.Length}");
        if (_tdaStack.Length > 0) ChangeWeapon();
    }

    /*CHEQUEOS DEL ISGROUND*/
    private void Update()
    {
        IsGrounded();
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

        return raycastHit.collider != null;
    }
    //INVIERTE EL PLAYER CUANDO SE MUEVE PARA ALGUNA DIRECCION
    private void PlayerFlip()
    {

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isPlayerLeft = !isPlayerLeft;
    }

    public void MoveRight()
    {

        if (isPlayerLeft)
        {
            PlayerFlip();
        }
        GameManager.instance.AddEventQueue(_cmdMoveRight);

    }
}
