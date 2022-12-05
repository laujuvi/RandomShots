using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private MovementController _movementController;
    private Vector2 _direction;
    private Rigidbody2D _playerBody;
    private float _jumpForce;


    public CmdJump(MovementController mc, Vector2 direction, Rigidbody2D playerBody, float jumpForce)
    {
        _movementController = mc;
        _direction = direction;
        _playerBody = playerBody;
        _jumpForce = jumpForce;

    }

    public void Execute() => _movementController.Jump(_playerBody, _jumpForce, _direction);

    public void Undo()
    {
        // Memento
    }
}
