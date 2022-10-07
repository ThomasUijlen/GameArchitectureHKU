public interface ICrafter
{
    public sRecipeList recipes { get; }

    public bool Craft(sRecipe recipe = null);
}