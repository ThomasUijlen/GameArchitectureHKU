using System.Collections.Generic;
using UnityEngine;

public class DefaultCrafter : BasicObject, ICrafter
{
    private GameManager gameManager;
    private CrafterMenu menu;

    private Inventory inventory;

    public sRecipeList recipes
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

    public DefaultCrafter(GameManager _gameManager) : base(_gameManager)
    {
        gameManager = _gameManager;

        InstantiateMenu(_gameManager);
    }

    public DefaultCrafter(GameManager _gameManager, Inventory _inventory) : base(_gameManager)
    {
        gameManager = _gameManager;
        inventory = _inventory;
       
        InstantiateMenu(_gameManager);
    }

    public bool Craft(sRecipe recipe)
    {
        inventory.ShowContent();

        // Check if inventory has the correct items
        foreach(ItemAmountPair pair in recipe.ingredients)
        {
            if (!inventory.HasItems(pair.item, pair.amount))
            {
                Debug.Log("Player did not have the right ingredients");
                return false;
            }
        }

        // Remove all the ingredients from the inventory
        foreach (ItemAmountPair pair in recipe.ingredients)
        {
            if (!inventory.RemoveItem(pair.item, pair.amount))
            {
                Debug.Log("Removing Items from inventory went wrong ;(");
            }
        }

        // Add the result to the inventory
        sItem item = recipe.craftingResult as sItem;
        inventory.AddItem(item, 1);

        inventory.ShowContent();

        return true;
    }

    private void InstantiateMenu(GameManager _gameManager)
    {
        if (_gameManager.prefabLibrary.HasPrefab("CrafterMenuUI"))
        {
            Canvas menuUI = _gameManager.prefabLibrary.InstantiatePrefab("CrafterMenuUI").GetComponent<Canvas>();
            menu = new CrafterMenu(_gameManager, menuUI, this);
        }
    }
}
