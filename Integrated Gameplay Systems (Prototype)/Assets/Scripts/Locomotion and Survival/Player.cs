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

    private GameObject player;

    int radius = 1;

    public Player(GameManager _gameManager) : base(_gameManager)
    {
        player = gameManager.prefabLibrary.InstantiatePrefab("Player");

        player.transform.position = player.transform.position + new Vector3(0, 0, 0);

        oxygen.SetOxygenAtStart();

        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(this, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(this, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(this, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(this, Vector3.back));

        menuStateMachine = new MenuStateMachine(_gameManager);

        groundMovement = new GroundMovement();
        waterMovement = new WaterMovement();

        locomotion = groundMovement;
    }

    void CheckTag()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, radius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Ground")
            {
                locomotion = groundMovement;
            }
            if (collider.tag == "Water")
            {
                locomotion = waterMovement;
            }
        }
    }
}