public interface ICrafter
{
    public sRecipeList recipes { get; }

    public bool Craft(sRecipe _recipe);
}