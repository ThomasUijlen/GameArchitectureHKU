using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultCrafter : BasicObject, ICrafter
{
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

    private CrafterMenu menu;
    private OpenMenuCommand openMenu;
    private Player player;

    public DefaultCrafter(GameManager _gameManager) : base(_gameManager)
    {
        InstantiateCrafter();
    }

    public DefaultCrafter(GameManager _gameManager, Player _player) : base(_gameManager)
    {
        player = _player;
        //openMenu = new OpenMenuCommand(typeof(CrafterMenu), _player.menuStateMachine, gameManager);

        InstantiateCrafter();
    }

    public bool Craft(sRecipe recipe)
    {
        player.inventory.ShowContent();

        // Check if inventory has the correct items
        foreach(ItemAmountPair pair in recipe.ingredients)
        {
            if (!player.inventory.HasItems(pair.item, pair.amount))
            {
                Debug.Log("Player did not have the right ingredients");
                return false;
            }
        }

        // Remove all the ingredients from the inventory
        foreach (ItemAmountPair pair in recipe.ingredients)
        {
            if (!player.inventory.RemoveItem(pair.item, pair.amount))
            {
                Debug.Log("Removing Items from inventory went wrong ;(");
            }
        }

        // Add the result to the inventory
        sItem item = recipe.craftingResult as sItem;
        player.inventory.AddItem(item, 1);

        player.inventory.ShowContent();

        return true;
    }

    private void InstantiateCrafter()
    {
        GameObject crafter = gameManager.prefabLibrary.InstantiatePrefab("Crafter");

        EventTrigger crafterTriggers = crafter.GetComponent<EventTrigger>();
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((data) => OpenCrafterMenu((PointerEventData)data));
        crafterTriggers.triggers.Add(pointerClickEntry);
    }

    private void OpenCrafterMenu(PointerEventData _pointerData)
    {
        //openMenu.Execute();
        CrafterMenu crafterMenu = new CrafterMenu(player.menuStateMachine, gameManager, this);
    }
}
