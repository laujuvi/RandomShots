using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdDoDamage : ICommand
{
    private IDamageable _lifeController;
    private int _damage;
    public CmdDoDamage(IDamageable lifeController, int damage)
    {
        _lifeController = lifeController;
        _damage = damage;
    }

    public void Execute() => _lifeController.TakeDamage(_damage);

    public void Undo()
    {
        // memento
    }
}
