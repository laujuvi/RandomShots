using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMove : ICommand
{
    private MovementController _movementController;
    private Vector3 _direction;

    public CmdMove(MovementController mc, Vector3 direction)
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
