using System;
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
        _gameManager.RegisterTag("Player", this);
        _gameManager.RegisterTag("Camera", GameObject.Find("MainCamera"));

        oxygen = new Oxygen();

        playerRotator = new PlayerRotator(_gameManager, this);
        menuStateMachine = new MenuStateMachine(_gameManager);
        moveStateMachine = new MoveStateMachine(_gameManager);

        inventory = new Inventory(_gameManager);
        gameManager.RegisterTag("Inventory", inventory);

        moveStateMachine.SetState(new GroundMovement(moveStateMachine, _gameManager, this));

        oxygen.SetOxygenAtStart();
    }

    public override void Update()
    {
        base.Update();

        oxygen.CheckOxygen();

        if (moveStateMachine.GetState().GetType() == typeof(GroundMovement))
        {
            oxygen.SetOxygenAtStart();
        }
        if (moveStateMachine.GetState().GetType() == typeof(WaterMovement))
        {
            oxygen.TimerOxygen();
        }

        oxygenUI.GetComponentInChildren<Text>().text = "Oxygen: " + oxygen.currentOxygenLevel.ToString();
    }

    public bool GroundCheck()
    {
        return Physics.Raycast(playerGameObject.transform.position, Vector3.down, 2f, LayerMask.GetMask("Terrain", "Interior", "Structure"));
    }
}