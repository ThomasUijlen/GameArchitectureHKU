using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    private IStateMachine stateMachine;

    public State(IStateMachine _stateMachine) {
        stateMachine = _stateMachine;
    }

    public abstract void EnableState();
    public abstract void DisableState();
    public virtual void Update() {}
    public virtual void FixedUpdate() {}
}
