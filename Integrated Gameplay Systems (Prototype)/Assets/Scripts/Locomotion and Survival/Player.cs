using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    public Inventory inventory;
    public MenuStateMachine menuStateMachine;

    Oxygen oxygen;

    Rigidbody rigidBody;

    Vector3 moveDirection;


    public Player(GameManager _gameManager) : base(_gameManager) 
    {
        //oxygen.SetOxygenAtStart();

        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(this, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(this, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(this, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(this, Vector3.back));

        menuStateMachine = new MenuStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);
    }

    public void DoMovement(Vector3 direction)
    {
        moveDirection = direction;
    }
}