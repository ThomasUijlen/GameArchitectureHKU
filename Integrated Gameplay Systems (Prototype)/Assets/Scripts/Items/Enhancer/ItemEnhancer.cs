using System.Collections.Generic;
using UnityEngine;

public class ItemEnhancer : ACrafter
{
    public override sRecipeList recipes {
        get
        {
            sRecipeList list = new sRecipeList();
            list.recipes.Add(gameManager.scriptableObjectLibrary.GetScriptableObject("enhancedWoodRecipe") as sRecipe);
            return list;
        }
    }
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Enhancer");

    public ItemEnhancer(GameManager _gameManager, Player _player) : base(_gameManager, _player)
    {
    }
}
