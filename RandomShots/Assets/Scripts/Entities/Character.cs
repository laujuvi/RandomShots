using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] private List<Gun> _gunPrefabs;
    [SerializeField] private Gun _gun;

    [SerializeField] private LayerMask _platformLayerMask;

    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _playerBody;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private int _maxStack;
    [SerializeField] private int _maxQueue;



    /* COMMAND LIST */

    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    private TDAStack<Gun> _tdaStack;
    private TDAQueue<Ammo> _tdaQueue;
    private Collider2D _lastCollider;

    private bool isPlayerLeft = false;
    public bool IsPLayerLeft => isPlayerLeft;

    private void Start()
    {
        ChangeWeapon(0);

        var mc = GetComponent<MovementController>();

        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

        _tdaStack = new TDAStack<Gun>(_maxStack);
        _tdaQueue = new TDAQueue<Ammo>(_maxQueue);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_lastCollider == collision.collider) return;
        var queue = collision.collider.gameObject.GetComponent<Ammo>();
        var stack = collision.collider.gameObject.GetComponent<Gun>();

        if (queue != null)
        {
            //if (_tdaQueue.Length >= _maxQueue)
            //{
            //    return;
            //}
            //else
            //{
                Debug.Log("Colision con queue Ammo");
                PickUpQueue(queue);
                Debug.Log(_tdaQueue.First());
                //PickUpStack(stack);   
            //}
        }

        if (stack != null)
        {
            //if (_tdaStack.Length > _maxStack)
            //{
            //    return;
            //} else
            //{
                Debug.Log("Colision con queue Gun");
                Debug.Log(PickUpStack(stack));
                //PickUpStack(stack);   
            //}


        }

        _lastCollider = collision.collider;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == _lastCollider) _lastCollider = null;
    }

    public void Attack() => GameManager.instance.AddEventQueue(_cmdAttack);
    public void Reload() => _gun?.Reload();
    public void Jump()
    {

        GameManager.instance.AddEventQueue(_cmdMoveJump);

        //if (IsGrounded())
        //{
        //    GameManager.instance.AddEventQueue(_cmdMoveJump);
        //}
    }

    public void MoveLeft() {

        if (!isPlayerLeft)
        {
            PlayerFlip();
        }
        GameManager.instance.AddEventQueue(_cmdMoveLeft);



    }
    public void MoveRight() {

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

    public Ammo PickUpQueue(Ammo ammo)
    {
        _tdaQueue.Queue(ammo);
        return _tdaQueue.First();
    }

    public void ChangeWeapon(int index)
    {

        _gun = Instantiate(_gunPrefabs[index], _weaponTransform.position, _weaponTransform.rotation, transform);
        _gun.SetOwner(this);
        _gun.Reload();
        _cmdAttack = new CmdAttack(_gun);
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size , 0f, Vector2.down, 0.1f, _platformLayerMask);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        } else
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

}
