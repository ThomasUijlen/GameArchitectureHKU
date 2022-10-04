using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : State
{
    public bool allowMovement = false;
    protected GameManager gameManager;
    public Menu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine) {
        gameManager = _gameManager;
    }
}
