using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMenu : Menu
{
    private OpenMenuCommand buildMenuCommand;

    public NoMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        buildMenuCommand = new OpenMenuCommand(typeof(BuildMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.B, buildMenuCommand);
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.B, buildMenuCommand);
    }
}
