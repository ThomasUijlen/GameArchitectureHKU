using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : State, ILocomotion
{
    public float speed = 2;

    int radius = 1;

    private Player player;
    protected GameManager gameManager;

    private MoveStateMachine moveStateMachine;

    private MoveCommand command1;
    private MoveCommand command2;
    private MoveCommand command3;
    private MoveCommand command4;

    public GroundMovement(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine)
    {
        gameManager = _gameManager;

        command1 = new MoveCommand(player, Vector3.forward);
        command2 = new MoveCommand(player, Vector3.left);
        command3 = new MoveCommand(player, Vector3.right);
        command4 = new MoveCommand(player, Vector3.back);
    }

    public override void FixedUpdate()
    {
        CheckTag();
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, command3);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, command4);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.S, command3);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.D, command4);
    }

    public void DoMove()
    {
        float x = player.playerGameObject.transform.position.x;
        float z = player.playerGameObject.transform.position.z;
        Vector3 movement = new Vector3(x, 0, z);
        movement = Vector3.ClampMagnitude(movement, 1);
        player.playerGameObject.transform.Translate(movement * speed * Time.deltaTime);
    }

    void CheckTag()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.playerGameObject.transform.position, radius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Ground")
            {
                moveStateMachine.SetState(groundMovement);
            }
            if (collider.tag == "Water")
            {
                moveStateMachine.SetState(waterMovement);
            }
        }
    }
}