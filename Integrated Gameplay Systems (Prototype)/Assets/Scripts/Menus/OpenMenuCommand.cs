using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpenMenuCommand : ICommand
{
    private Type menuType;
    private IStateMachine stateMachine;
    private GameManager gameManager;

    public OpenMenuCommand(Type _menuType, IStateMachine _stateMachine, GameManager _gameManager) {
        menuType = _menuType;
        stateMachine = _stateMachine;
        gameManager = _gameManager;
    }

    public void Execute() {
        if(!menuType.IsSubclassOf(typeof(Menu))) return;
        object obj = Activator.CreateInstance(menuType, stateMachine, gameManager);
        stateMachine.SetState((Menu) obj);
    }
}
