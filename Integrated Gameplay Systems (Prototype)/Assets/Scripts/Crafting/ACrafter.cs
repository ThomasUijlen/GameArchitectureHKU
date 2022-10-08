using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ACrafter : BasicObject, ICrafter
{
    public abstract sRecipeList recipes { get; }

    protected abstract GameObject CrafterPrefab { get; }
    protected GameObject CrafterObject;
    protected Player player;

    public ACrafter(GameManager _gameManager, Player _player) : base(_gameManager)
    {
        player = _player;

        InstantiateCrafter();
    }

    public virtual bool Craft(sRecipe recipe)
    {
        player.inventory.ShowContent();

        // Check if inventory has the correct items
        foreach (ItemAmountPair pair in recipe.ingredients)
        {
            if (!player.inventory.HasItems(pair.itemBase, pair.amount))
            {
                Debug.Log("Player did not have the right ingredients");
                return false;
            }
        }

        // Remove all the ingredients from the inventory
        foreach (ItemAmountPair pair in recipe.ingredients)
        {
            if (!player.inventory.RemoveItemBase(pair.itemBase, pair.amount))
            {
                Debug.Log("Removing Items from inventory went wrong ;(");
            }
        }

        // Add the result to the inventory
        //Item item = recipe.craftingResult as Item;   // This is against factory

        recipe.craftingResult.AddResultToWorld(gameManager);

        //player.inventory.AddItem(item);

        player.inventory.ShowContent();

        return true;
    }

    protected virtual void InstantiateCrafter()
    {
        CrafterObject = GameObject.Instantiate(CrafterPrefab);

        EventTriggerDecorator.AddTrigger(CrafterObject, EventTriggerType.PointerClick, 
                                            (data) => OpenCrafterMenu((PointerEventData)data));
    }

    protected virtual void OpenCrafterMenu(PointerEventData _pointerData)
    {
        CrafterMenu crafterMenu = new CrafterMenu(player.menuStateMachine, gameManager, this);
        player.menuStateMachine.SetState(crafterMenu);
    }
}
