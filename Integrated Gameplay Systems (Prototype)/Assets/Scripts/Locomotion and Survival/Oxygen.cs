public class Oxygen
{
    int currentOxygenLevel;
    int maxOxygenLevel = 100;

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