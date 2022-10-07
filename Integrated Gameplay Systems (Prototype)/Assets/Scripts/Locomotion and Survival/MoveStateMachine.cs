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

    public ILocomotion GetLocomotion()
    {
        return (ILocomotion)currentLocomotion;
    }
}
