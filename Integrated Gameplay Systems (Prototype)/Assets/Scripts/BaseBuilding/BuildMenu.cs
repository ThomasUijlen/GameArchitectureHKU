using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : Menu
{
    private OpenMenuCommand backCommand;
    public BuildMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = false;
        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        Debug.Log("build menu!");
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
    }
}
