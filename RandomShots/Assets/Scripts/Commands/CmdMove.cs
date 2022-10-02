using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMove : ICommand
{
    private MovementController _movementController;
    private Vector2 _direction;

    public CmdMove(MovementController mc, Vector2 direction)
    {
        _movementController = mc;
        _direction = direction;
    }

    public void Execute() => _movementController.Move(_direction);

    public void Undo()
    {
        // Memento
    }
}
