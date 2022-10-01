using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] private List<Gun> _gunPrefabs;
    [SerializeField] private Gun _gun;

    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _playerBody;

    /* COMMAND LIST */
    private CmdMove _cmdMoveForward;
    private CmdMove _cmdMoveBack;
    private CmdMove _cmdMoveLeft;
    private CmdMove _cmdMoveRight;
    private CmdJump _cmdMoveJump;
    private CmdAttack _cmdAttack;

    private void Start()
    {
        ChangeWeapon(0);

        var mc = GetComponent<MovementController>();

        _cmdMoveLeft = new CmdMove(mc, Vector2.left);
        _cmdMoveRight = new CmdMove(mc, Vector2.right);
        _cmdMoveJump = new CmdJump(mc, Vector2.up, _playerBody, _jumpForce);

    }

    public void Attack() => GameManager.instance.AddEventQueue(_cmdAttack);
    public void Reload() => _gun?.Reload();
    public void Jump() => GameManager.instance.AddEventQueue(_cmdMoveJump);

    public void MoveForward() => GameManager.instance.AddEventQueue(_cmdMoveForward);
    public void MoveBack() => GameManager.instance.AddEventQueue(_cmdMoveBack);
    public void MoveLeft() => GameManager.instance.AddEventQueue(_cmdMoveLeft);
    public void MoveRight() => GameManager.instance.AddEventQueue(_cmdMoveRight);

    public void ChangeWeapon(int index)
    {
       // Destroy(_gun?.gameObject);
       // DestroyImmediate(_gun?.gameObject, true);
        _gun = Instantiate(_gunPrefabs[index], transform);
        _gun.Reload();
        _cmdAttack = new CmdAttack(_gun);
    }
}

/*

    - interfaz: iCommand
    - comando concreto: AttackCommand
    - invoker: new command, command.execute
    - receptor: ejecuta la orden
 
*/
