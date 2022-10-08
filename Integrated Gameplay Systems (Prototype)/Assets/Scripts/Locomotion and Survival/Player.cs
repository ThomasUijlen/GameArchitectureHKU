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

        //playerGameObject.transform.position = new Vector3(0,2,0);

        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);

        moveStateMachine.SetState(new GroundMovement(moveStateMachine, _gameManager, this));
        Debug.Log(moveStateMachine.currentLocomotion);
    }

}