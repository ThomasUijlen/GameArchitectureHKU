using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class sRecipe: ScriptableObject
{
    public List<ItemAmountPair> ingredients = new List<ItemAmountPair>();
    public sItemBase craftingResult;
    public List<sEnhancer> itemDecorators;

    public sRecipe() { }

    public sRecipe(List<ItemAmountPair> _ingredients, sItemBase _craftingResult)
    {
        ingredients = _ingredients;
        craftingResult = _craftingResult;
    }

    public string IngredientsString()
    {
        string result = string.Empty;
        foreach (ItemAmountPair ingredient in ingredients)
        {
            result += $"{ingredient.itemBase.name}\n" +
                        $"Amount: {ingredient.amount}\n\n";
        }

        return result;
    }
}