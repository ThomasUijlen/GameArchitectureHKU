using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen
{
    private Player player;

    public int currentOxygenLevel;
    private int maxOxygenLevel = 100;

    public Oxygen(GameManager _gameManager)
    {

    }

    public void SetOxygenAtStart()
    {
        currentOxygenLevel = maxOxygenLevel;
    }

    public void AddOxygen(int amount)
    {
        currentOxygenLevel += amount;
    }

    public void SubstractOxygen(int amount)
    {
        currentOxygenLevel -= amount;
    }

}