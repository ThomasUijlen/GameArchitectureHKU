using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Player player;
    public Vector3 direction;

    public MoveCommand(Player _player, Vector3 _direction)
    {
        player = _player;
        direction = _direction;
    }

    public void Execute()
    {
        player.moveStateMachine.GetLocomotion().AddDirection(direction);
    }
}