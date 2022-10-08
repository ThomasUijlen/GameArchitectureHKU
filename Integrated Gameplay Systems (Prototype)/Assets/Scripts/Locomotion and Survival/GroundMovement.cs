using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : State, ILocomotion
{ 
    private Player player;

    protected GameManager gameManager;

    public Collider[] hitColliders;

    private Oxygen oxygen;

    private MoveCommand command1;
    private MoveCommand command2;
    private MoveCommand command3;
    private MoveCommand command4;
    public float speed = 15;

    Vector3 currentDirection;

    private Rigidbody rigidbody;

    public GroundMovement(IStateMachine _stateMachine, GameManager _gameManager, Player _player) : base(_stateMachine)
    {
        gameManager = _gameManager;

        player = _player;

        command1 = new MoveCommand(player, Vector3.forward);
        command2 = new MoveCommand(player, Vector3.left);
        command3 = new MoveCommand(player, Vector3.right);
        command4 = new MoveCommand(player, Vector3.back);
        rigidbody = player.playerGameObject.GetComponent<Rigidbody>();
    }

    public override void FixedUpdate()
    {
        if(player.menuStateMachine.GetState().allowMovement) DoMove();
        CheckTag();
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, command1, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, command2, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, command4, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, command3, InputManager.INPUT_MODE.PRESSED);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.S, command4);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.D, command3);
    }

    public void DoMove()
    {
        Vector3 movementDirection = currentDirection.normalized;
        currentDirection = Vector3.zero;

        Vector3 velocityX = movementDirection.x * player.playerGameObject.transform.right * speed;
        Vector3 velocityY = Vector3.down*9.81f;
        Vector3 velocityZ = movementDirection.z * player.playerGameObject.transform.forward * speed;
        rigidbody.velocity = velocityX+velocityY+velocityZ;
    }

    public void AddDirection(Vector3 _direction)
    {
        currentDirection += _direction;
    }

    void CheckTag()
    {
        bool isUnderwater = false;

        foreach (Collider collider in Physics.OverlapSphere(player.playerGameObject.transform.position, 10f))
        {
            if (collider.bounds.Contains(player.playerGameObject.transform.position))
            {
                if (collider.tag == "Water")
                {
                    isUnderwater = true;
                    break;
                }
            }
        }

        foreach (Collider collider in Physics.OverlapSphere(player.playerGameObject.transform.position, 10f, LayerMask.GetMask("Interior")))
        {
            if (collider.bounds.Contains(player.playerGameObject.transform.position))
            {
                isUnderwater = false;
                break;
            }
        }

        if (isUnderwater)
        {
            player.moveStateMachine.SetState(new WaterMovement(player.moveStateMachine, gameManager, player));
        }
    }
}