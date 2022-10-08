using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateMachine : BasicObject, IStateMachine
{
    public State currentLocomotion;
    public MoveStateMachine(GameManager _gameManager) : base(_gameManager)
    {
      
    }

    public void SetState(State _newState)
    {
        if (currentLocomotion != null) currentLocomotion.DisableState();
        currentLocomotion = (State)_newState;
        if (currentLocomotion != null) currentLocomotion.EnableState();
    }

    public override void Update()
    {
        if (currentLocomotion != null) currentLocomotion.Update();
    }

    public override void FixedUpdate()
    {
        if (currentLocomotion != null) currentLocomotion.FixedUpdate();
    }

    public ILocomotion GetLocomotion()
    {
        return (ILocomotion)currentLocomotion;
    }
}
