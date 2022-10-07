using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    Oxygen oxygen;

    Rigidbody rigidBody;

    Vector3 moveDirection;

    private MenuStateMachine menuStateMachine;

    private GameObject player;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        player = gameManager.prefabLibrary.InstantiatePrefab("Player");

        player.transform.position = player.transform.position + new Vector3(0,0,0);

        oxygen.SetOxygenAtStart();

        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(this, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(this, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(this, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(this, Vector3.back));

        menuStateMachine = new MenuStateMachine(_gameManager);
    }

    public void DoMovement(Vector3 direction)
    {
        moveDirection = direction;
    }
}