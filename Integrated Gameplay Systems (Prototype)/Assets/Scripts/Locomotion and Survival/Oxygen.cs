using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oxygen
{
    public int currentOxygenLevel;
    private int maxOxygenLevel = 50;
    private float elapsed;

    public Oxygen(GameManager _gameManager)
    {

    }

    public void SetOxygenAtStart()
    {
        currentOxygenLevel = maxOxygenLevel;
    }

    public void CheckOxygen()
    {
        if(currentOxygenLevel <= 0)
        {
            currentOxygenLevel = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TimerOxygen()
    {
        //Debug.Log(elapsed);
        elapsed += Time.deltaTime;
        if (elapsed >= 5f)
        {
            elapsed -= 5f;
            OxygenDepletion(5);
        }
    }

    void OxygenDepletion(int amount)
    {
        currentOxygenLevel = currentOxygenLevel - amount;
    }
}