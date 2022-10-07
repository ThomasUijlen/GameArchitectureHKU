using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DemolishMenu : Menu
{
    private OpenMenuCommand backCommand;

    public DemolishMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = true;
        backCommand = new OpenMenuCommand(typeof(BuildMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
    }
}
