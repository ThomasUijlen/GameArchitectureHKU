using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    public Inventory inventory;
    public MenuStateMachine menuStateMachine;
    public MoveStateMachine moveStateMachine;

    Oxygen oxygen;

    private GameObject player;

    int radius = 1;
    public GameObject playerGameObject;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        player = gameManager.prefabLibrary.InstantiatePrefab("Player");
        playerGameObject = gameManager.prefabLibrary.InstantiatePrefab("Player");

        oxygen.SetOxygenAtStart();

        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(this, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(this, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(this, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(this, Vector3.back));

        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);
    }

}