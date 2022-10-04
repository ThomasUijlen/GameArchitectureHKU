using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateMachine : BasicObject, IStateMachine
{
    private Menu currentMenu;

    public MenuStateMachine(GameManager _gameManager) : base(_gameManager) {
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

    public void SetState(State _newState) {
        if(!(_newState is Menu)) return;

        if(currentMenu != null) currentMenu.DisableState();
        currentMenu = (Menu) _newState;
        if(currentMenu != null) currentMenu.EnableState();
    }

    public Menu GetState() {
        return currentMenu;
    }
}
