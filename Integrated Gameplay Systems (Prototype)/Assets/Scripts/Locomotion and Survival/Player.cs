using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    public Inventory inventory;
    public MenuStateMachine menuStateMachine;
    public MoveStateMachine moveStateMachine;

    public GameObject playerGameObject;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        playerGameObject = gameManager.prefabLibrary.InstantiatePrefab("Player");
        _gameManager.RegisterTag("Camera", GameObject.Find("MainCamera"));

        //playerGameObject.transform.position = new Vector3(0,2,0);

        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);
        gameManager.RegisterTag("Inventory", inventory);

        moveStateMachine.SetState(new GroundMovement(moveStateMachine, _gameManager, this));
    }

}