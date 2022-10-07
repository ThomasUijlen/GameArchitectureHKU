using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : State, ILocomotion
{
    public float speed = 20;

    Vector3 currentDirection;

    private Player player;

    protected GameManager gameManager;

    private MoveCommand command1;
    private MoveCommand command2;
    private MoveCommand command3;
    private MoveCommand command4;

    public GroundMovement(IStateMachine _stateMachine, GameManager _gameManager, Player _player) : base(_stateMachine)
    {
        gameManager = _gameManager;

        player = _player;

        command1 = new MoveCommand(player, Vector3.forward);
        command2 = new MoveCommand(player, Vector3.left);
        command3 = new MoveCommand(player, Vector3.right);
        command4 = new MoveCommand(player, Vector3.back);
    }

    public override void FixedUpdate()
    {
        DoMove();    
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, command3);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, command4);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.S, command3);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.D, command4);
    }

    public void DoMove()
    {
        Vector3 movement = currentDirection.normalized;
        player.playerGameObject.transform.Translate(movement * speed * Time.deltaTime);
        currentDirection = Vector3.zero;
    }

    public void AddDirection(Vector3 _direction)
    {
        currentDirection += _direction;
    }


}