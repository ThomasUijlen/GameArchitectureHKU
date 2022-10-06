using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class sRecipe: ScriptableObject
{
    public List<ItemAmountPair> ingredients = new List<ItemAmountPair>();
    public ICraftingResult craftingResult;

    public sRecipe() { }

    public sRecipe(List<ItemAmountPair> _ingredients, ICraftingResult _craftingResult)
    {
        ingredients = _ingredients;
        craftingResult = _craftingResult;
    }

    public string IngredientsString()
    {
        string result = string.Empty;
        foreach (ItemAmountPair ingredient in ingredients)
        {
            result += $"{ingredient.item.name}\n" +
                        $"Amount: {ingredient.amount}\n\n";
        }

        return result;
    }
}