using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    public Inventory inventory;
    public MenuStateMachine menuStateMachine;
    public MoveStateMachine moveStateMachine;
    public Oxygen oxygen;

    public GameObject playerGameObject;
    private GameObject oxygenCanvas;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        playerGameObject = _gameManager.prefabLibrary.InstantiatePrefab("Player");
        oxygenCanvas = _gameManager.prefabLibrary.InstantiatePrefab("OXYGENUI");

        _gameManager.RegisterTag("Camera", GameObject.Find("MainCamera"));

        //playerGameObject.transform.position = new Vector3(0,2,0);

        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        oxygen = new Oxygen(_gameManager);

        inventory = new Inventory(_gameManager);
        gameManager.RegisterTag("Inventory", inventory);

        moveStateMachine.SetState(new GroundMovement(moveStateMachine, _gameManager, this));
    }

}