using System.Collections.Generic;
using UnityEngine;

public class DefaultCrafter : ACrafter
{
    public override sRecipeList recipes
    {
        get
        {
            sRecipeList list = new sRecipeList();

            List<ItemAmountPair> ingredients = new List<ItemAmountPair>
            {
                new ItemAmountPair(ItemLibrary.Wood, 2)
            };
            sRecipe basicRecipe = new sRecipe()
            {
                ingredients = ingredients,
                craftingResult = ItemLibrary.Stick
            };

            List<ItemAmountPair> ingredients2 = new List<ItemAmountPair>
            {
                new ItemAmountPair(ItemLibrary.Stick, 2),
                new ItemAmountPair(ItemLibrary.Stone, 2)
            };
            sRecipe otherRecipe = new sRecipe()
            {
                ingredients = ingredients2,
                craftingResult = ItemLibrary.Tool
            };

            list.recipes.Add(basicRecipe);
            list.recipes.Add(otherRecipe);

            return list;
        }
    }

    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Crafter");
    public DefaultCrafter(GameManager _gameManager, Player _player) : base(_gameManager, _player) { }
}
