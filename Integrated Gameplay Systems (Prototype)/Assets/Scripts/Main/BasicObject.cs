using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicObject
{
    private GameManager gameManager;
    public BasicObject(GameManager _gameManager) {
        gameManager = _gameManager;
    }

    public virtual void Update() {}

    public virtual void FixedUpdate() {}
}
