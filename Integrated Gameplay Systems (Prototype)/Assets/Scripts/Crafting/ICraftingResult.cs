public interface ICraftingResult
{
    public string name { get; }

    public void AddResultToWorld(GameManager _gameManager);
}
