using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oxygen
{
    private Player player;


    public int currentOxygenLevel;
    private int maxOxygenLevel = 100;
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
        elapsed += Time.deltaTime;
        if (elapsed >= 10f)
        {
            elapsed -= 10f;
            //OxygenDepletion(1);
        }
    }

    void OxygenDepletion(int amount)
    {
        currentOxygenLevel = currentOxygenLevel - amount;
    }
}