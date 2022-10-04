using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicObject, IStateMachine
{
    Oxygen oxygen;

    Rigidbody rigidBody;

    Vector3 moveDirection;

    private Menu currentMenu;

    public Player(GameManager _gameManager) : base(_gameManager) 
    {
        //oxygen.SetOxygenAtStart();

        gameManager.inputManager.RegisterKeyBinding(KeyCode.W, new MoveCommand(this, Vector3.forward));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.A, new MoveCommand(this, Vector3.left));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.S, new MoveCommand(this, Vector3.right));
        gameManager.inputManager.RegisterKeyBinding(KeyCode.D, new MoveCommand(this, Vector3.back));

        SetState(new NoMenu(this, gameManager));
    }

    public override void Update()
    {
        if(currentMenu != null) currentMenu.Update();
    }

    public override void FixedUpdate()
    {
        if(currentMenu != null) currentMenu.FixedUpdate();
    }

    public void DoMovement(Vector3 direction)
    {
        moveDirection = direction;
    }

    //State machine logic -----------------------------------
    public void SetState(State _newState) {
        if(!(_newState is Menu)) return;

        if(currentMenu != null) currentMenu.DisableState();
        currentMenu = (Menu) _newState;
        if(currentMenu != null) currentMenu.EnableState();
    }
}