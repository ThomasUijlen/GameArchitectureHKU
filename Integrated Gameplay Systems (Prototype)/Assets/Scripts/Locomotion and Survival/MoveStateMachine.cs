using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateMachine : BasicObject, IStateMachine
{
    private GroundMovement groundMovement;
    private WaterMovement waterMovement;

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
}
