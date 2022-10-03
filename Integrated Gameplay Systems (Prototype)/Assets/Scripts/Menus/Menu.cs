using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu
{
    public bool allowMovement = false;
    private GameManager gameManager;

    public Menu(GameManager _gameManager) {
        gameManager = _gameManager;
    }

    public abstract void EnableMenu();
    public abstract void DisableMenu();
}
