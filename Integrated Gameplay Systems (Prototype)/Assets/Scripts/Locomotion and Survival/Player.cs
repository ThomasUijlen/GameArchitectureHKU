public class Player : BasicObject
{
    public Player(GameManager _gameManager) : base(_gameManager) { }

    Inventory inventory;

    ICommand icommand;
    Oxygen oxygen;

    public Player PlayerConstructor()
    {
        oxygen.SetOxygenAtStart();

        

        return this;
    }

    public override void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
        
    }
}