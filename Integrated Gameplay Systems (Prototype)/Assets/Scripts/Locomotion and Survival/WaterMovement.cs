using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : State, ILocomotion
{
    public float speed = 2;

    Vector3 currentDirection;

    private Player player;
    protected GameManager gameManager;

    public Collider[] hitColliders;

    private Oxygen oxygen;

    private MoveCommand command1;
    private MoveCommand command2;
    private MoveCommand command3;
    private MoveCommand command4;

    private float elapsed;

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    [Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
    [Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    public WaterMovement(IStateMachine _stateMachine, GameManager _gameManager, Player _player) : base(_stateMachine)
    {
        gameManager = _gameManager;

        player = _player;

        command1 = new MoveCommand(player, Vector3.forward);
        command2 = new MoveCommand(player, Vector3.left);
        command3 = new MoveCommand(player, Vector3.right);
        command4 = new MoveCommand(player, Vector3.back);
    }

    public override void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 10f)
        {
            elapsed = elapsed % 10f;
            oxygen.SubstractOxygen(5);
        }

        DoMove();
        DoCamera();
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
        Vector3 movement = currentDirection.normalized;
        player.playerGameObject.transform.Translate(movement * speed * Time.deltaTime);
        currentDirection = Vector3.zero;
    }

    public void AddDirection(Vector3 _direction)
    {
        currentDirection += _direction;
    }

    public void DoCamera()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);


        player.playerGameObject.transform.localRotation = xQuat * yQuat;

        Cursor.visible = false;
    }

    void CheckTag()
    {
        bool isUnderwater = true;

        foreach (Collider collider in Physics.OverlapSphere(player.playerGameObject.transform.position, 10f))
        {
            if (collider.bounds.Contains(player.playerGameObject.transform.position))
            {
                if (collider.tag != "Water")
                {
                    isUnderwater = false;
                    oxygen.currentOxygenLevel = 100;
                }
                break;
            }
        }

        if (!isUnderwater)
        {
            player.moveStateMachine.SetState(new GroundMovement(player.moveStateMachine, gameManager, player));
        }
    }
}