using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : BasicObject
{
    private float sensitivity = 2f;
    private Vector2 rotation = Vector2.zero;
    private const string X_AXIS = "Mouse X";
    private const string Y_AXIS = "Mouse Y";
    private const float Y_ROTATION_LIMIT = 88f;

    public Player player;
    public GameObject playerObject;
    public GameObject camera;

    public PlayerRotator(GameManager _gameManager, Player _player) : base(_gameManager) {
        player = _player;
        playerObject = player.playerGameObject;
        camera = GameObject.Find("MainCamera");
    }

    public override void Update()
    {
        DoCamera();
    }

    public void DoCamera()
    {
        if(!player.menuStateMachine.GetState().allowMovement) return;

        rotation.x += Input.GetAxisRaw(X_AXIS) * sensitivity;
        rotation.y += Input.GetAxisRaw(Y_AXIS) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -Y_ROTATION_LIMIT, Y_ROTATION_LIMIT);
        Quaternion xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        Quaternion yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        playerObject.transform.localRotation = xQuat;
        camera.transform.localRotation = yQuat;
    }
}