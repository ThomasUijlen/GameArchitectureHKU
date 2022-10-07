using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceMenu : Menu
{
    private OpenMenuCommand backCommand;
    private HologramStructure hologramStructure;
    public PlaceMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = true;
        backCommand = new OpenMenuCommand(typeof(BuildMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        hologramStructure.Destroy();
    }

    public void SetStructure(string structureName) {
        hologramStructure = new HologramStructure(structureName, gameManager);
    }
}
