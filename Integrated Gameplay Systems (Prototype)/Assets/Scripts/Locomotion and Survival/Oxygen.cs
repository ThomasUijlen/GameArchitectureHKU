using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen
{
    public int currentOxygenLevel;
    private int maxOxygenLevel = 100;

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

    private IEnumerator OxygenDepletion(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        currentOxygenLevel--;

    }
}