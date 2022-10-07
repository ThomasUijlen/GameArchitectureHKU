using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : State, ILocomotion
{
    public float speed = 2;

    private int radius = 1;

    private float elapsed = 0f;

    Vector3 currentDirection;

    private Player player;
    private Oxygen oxygen;
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
        elapsed += Time.deltaTime;
        if(elapsed >= 10f)
        {
            elapsed = elapsed % 10f;
            oxygen.SubstractOxygen(5);
            Debug.Log(oxygen.currentOxygenLevel);
        }

        //CheckTag();
        DoMove();    
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

    public void AddDirection(Vector3 _direction)
    {
        currentDirection += _direction;
    }

    //void CheckTag()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(player.playerGameObject.transform.position, radius);

    //    foreach (Collider collider in hitColliders)
    //    {
    //        if (collider.tag == "Ground")
    //        {
    //            moveStateMachine.SetState(groundMovement);
    //            oxygen.currentOxygenLevel = 100;
    //            Invokere

    //        }
    //        if (collider.tag == "Water")
    //        {
    //            moveStateMachine.SetState(waterMovement);
    //        }
    //    }
    //}
}