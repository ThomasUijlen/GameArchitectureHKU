using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class sRecipe: ScriptableObject
{
    public List<ItemAmountPair> ingredients = new List<ItemAmountPair>();
    public ICraftingResult craftingResult;
}