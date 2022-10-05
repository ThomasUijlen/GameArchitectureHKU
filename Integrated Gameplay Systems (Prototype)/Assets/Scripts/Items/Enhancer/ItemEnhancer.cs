using System.Collections.Generic;
using UnityEngine;

public class ItemEnhancer : ACrafter
{
    public override sRecipeList recipes { 
        get
        {
            sRecipeList list = new sRecipeList();

            List<ItemAmountPair> ingredients = new List<ItemAmountPair>
            {
                new ItemAmountPair(ItemLibrary.Wood, 1),
                new ItemAmountPair(ItemLibrary.Stone, 1)
            };
            sItem EnhancedWood = new GoldValueEnhancer(10).Enhance(ItemLibrary.Wood);

            sRecipe basicRecipe = new sRecipe()
            {
                ingredients = ingredients,
                craftingResult = EnhancedWood,
            };

            list.recipes.Add(basicRecipe);

            return list;
        }
    }
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Enhancer");

    public ItemEnhancer(GameManager _gameManager, Player _player) : base(_gameManager, _player)
    {
    }
}
