using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : State, ILocomotion
{
    public float speed = 10;

    Vector3 currentDirection;

    private Player player;
    protected GameManager gameManager;

    public Collider[] hitColliders;

    private Oxygen oxygen;

    private MoveCommand command1;
    private MoveCommand command2;
    private MoveCommand command3;
    private MoveCommand command4;
    private MoveCommand command5;
    private MoveCommand command6;

    private Rigidbody rigidbody;
    private GameObject playerCamera;

    public WaterMovement(IStateMachine _stateMachine, GameManager _gameManager, Player _player) : base(_stateMachine)
    {
        gameManager = _gameManager;

        player = _player;

        oxygen = new Oxygen(_gameManager);

        command1 = new MoveCommand(player, Vector3.forward);
        command2 = new MoveCommand(player, Vector3.left);
        command3 = new MoveCommand(player, Vector3.right);
        command4 = new MoveCommand(player, Vector3.back);
        command5 = new MoveCommand(player, Vector3.down);
        command6 = new MoveCommand(player, Vector3.up);
        rigidbody = player.playerGameObject.GetComponent<Rigidbody>();
        playerCamera = player.playerRotator.camera;
    }

    public override void FixedUpdate()
    {
        DoMove();
        CheckTag();
        oxygen.CheckOxygen();
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, command1, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, command2, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, command4, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, command3, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Q, command5, InputManager.INPUT_MODE.PRESSED);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.E, command6, InputManager.INPUT_MODE.PRESSED);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.W, command1);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.A, command2);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.S, command4);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.D, command3);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Q, command5);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.E, command6);
    }

    public void DoMove()
    {
        Vector3 movementDirection = currentDirection.normalized;
        currentDirection = Vector3.zero;

        Vector3 velocityX = movementDirection.x * playerCamera.transform.right * speed;
        Vector3 velocityY = movementDirection.y * playerCamera.transform.up * speed;
        Vector3 velocityZ = movementDirection.z * playerCamera.transform.forward * speed;
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
                    //oxygen.TimerOxygen();
                    Debug.Log(oxygen.currentOxygenLevel);
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

        if (!isUnderwater)
        {
            player.moveStateMachine.SetState(new GroundMovement(player.moveStateMachine, gameManager, player));
            oxygen.SetOxygenAtStart();
            Debug.Log(oxygen.currentOxygenLevel);
        }
    }
}