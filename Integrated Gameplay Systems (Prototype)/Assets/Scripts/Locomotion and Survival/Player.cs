using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasicObject
{
    public Inventory inventory;
    public MenuStateMachine menuStateMachine;
    public MoveStateMachine moveStateMachine;
    public PlayerRotator playerRotator;
    private Oxygen oxygen;

    public GameObject playerGameObject;
    public GameObject oxygenUI;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        playerGameObject = gameManager.prefabLibrary.InstantiatePrefab("Player");
        oxygenUI  = gameManager.prefabLibrary.InstantiatePrefab("OXYGENUI");
        _gameManager.RegisterTag("Camera", GameObject.Find("MainCamera"));

        oxygen = new Oxygen(_gameManager);

        playerRotator = new PlayerRotator(_gameManager, this);
        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);
        gameManager.RegisterTag("Inventory", inventory);

        oxygen.SetOxygenAtStart();
        Debug.Log(oxygen.currentOxygenLevel);

        moveStateMachine.SetState(new GroundMovement(moveStateMachine, _gameManager, this));
    }

    public override void Update()
    {
        base.Update();

        oxygenUI.GetComponentInChildren<Text>().text = "Oxygen: " + oxygen.currentOxygenLevel.ToString();
    }
}