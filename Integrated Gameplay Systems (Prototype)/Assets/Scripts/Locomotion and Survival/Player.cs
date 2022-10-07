using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject
{
    Oxygen oxygen;

    Vector3 moveDirection;

    public ILocomotion locomotion;
    private GroundMovement groundMovement;
    private WaterMovement waterMovement;

    private MenuStateMachine menuStateMachine;


    public GameObject playerGameObject;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        playerGameObject = gameManager.prefabLibrary.InstantiatePrefab("Player");

        playerGameObject.transform.position = playerGameObject.transform.position + new Vector3(0, 0, 0);

        oxygen.SetOxygenAtStart();

        menuStateMachine = new MenuStateMachine(_gameManager);
    }

}