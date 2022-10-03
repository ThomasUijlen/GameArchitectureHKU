using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipelist", menuName = "Recipes/Recipe List")]
public class sRecipeList : ScriptableObject
{
    public List<sRecipe> recipes;
}