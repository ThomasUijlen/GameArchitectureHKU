using System.Collections.Generic;
using UnityEngine;

public class DefaultCrafter : ACrafter
{
    public override sRecipeList recipes
    {
        get
        {
            sRecipeList list = new sRecipeList();

            list.recipes.Add(gameManager.scriptableObjectLibrary.GetScriptableObject("stickRecipe") as sRecipe);
            list.recipes.Add(gameManager.scriptableObjectLibrary.GetScriptableObject("toolRecipe") as sRecipe);

            return list;
        }
    }

    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Crafter");
    public DefaultCrafter(GameManager _gameManager, Player _player) : base(_gameManager, _player) { }
}
