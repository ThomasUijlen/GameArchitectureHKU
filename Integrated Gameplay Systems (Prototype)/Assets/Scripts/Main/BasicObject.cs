using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicObject
{
    public GameManager gameManager;
    public BasicObject(GameManager _gameManager) {
        gameManager = _gameManager;
        gameManager.RegisterBasicObject(this);
    }

    public virtual void Update() {}

    public virtual void FixedUpdate() {}

    public virtual void Destroy() {
        gameManager.DeregisterBasicObject(this);
    }
}
