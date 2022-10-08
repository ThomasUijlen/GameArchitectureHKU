using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemolishMenu : Menu, ICommand
{
    private GameObject playerCamera;
    private OpenMenuCommand backCommand;

    public DemolishMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = true;
        backCommand = new OpenMenuCommand(typeof(BuildMenu), _stateMachine, _gameManager);

        playerCamera = (GameObject) gameManager.GetObjectWithTag("Camera");
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Mouse0, this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Mouse0, this);

        Cursor.lockState = CursorLockMode.None;
    }

    public void Execute() {
        int layerMask = LayerMask.GetMask("Structure");

        RaycastHit hit;
        if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f, layerMask)) {
            GameObject.Destroy(hit.transform.gameObject);
        }
    }
}
