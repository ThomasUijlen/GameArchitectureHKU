using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : State, ILocomotion
{
    public float speed = 2;

    Vector3 currentDirection;

    private Player player;
    protected GameManager gameManager;

    public WaterMovement(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine)
    {
        gameManager = _gameManager;
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(player, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(player, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(player, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(player, Vector3.back));
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.W, new MoveCommand(player, Vector3.forward));
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.A, new MoveCommand(player, Vector3.left));
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.S, new MoveCommand(player, Vector3.right));
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.D, new MoveCommand(player, Vector3.back));
    }

    public void DoMove()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 forwardMovement = camTransform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        player.playerGameObject.transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void AddDirection(Vector3 _direction)
    {
        currentDirection += _direction;
    }
}