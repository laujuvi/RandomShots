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

    [SerializeField] private Animator _animator;

    /* COMMAND LIST */

    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    private bool isPlayerLeft = false;
    public bool IsPLayerLeft => isPlayerLeft;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        ChangeWeapon(0);

        var mc = GetComponent<MovementController>();

        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

    }

    public void Attack() => GameManager.instance.AddEventQueue(_cmdAttack);
    public void Reload() => _gun?.Reload();
    public void Jump()
    {
        _animator.SetFloat("Shoot", 1);

        if (IsGrounded())
        {
            GameManager.instance.AddEventQueue(_cmdMoveJump);
        }
    }

    public void MoveLeft() {

        if (!isPlayerLeft)
        {
            PlayerFlip();
            _animator.SetFloat("XDirection", 1);
        }
        GameManager.instance.AddEventQueue(_cmdMoveLeft);



    }
    public void MoveRight() {

        if (isPlayerLeft)
        {
            PlayerFlip();
            _animator.SetFloat("XDirection", -1);

        }
        GameManager.instance.AddEventQueue(_cmdMoveRight);

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
        _animator.SetFloat("XDirection", 0);
        _animator.SetFloat("Shoot", 0);
        IsGrounded();
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
