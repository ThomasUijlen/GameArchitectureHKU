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
            if (!player.inventory.RemoveItem(pair.itemBase, pair.amount))
            {
                Debug.Log("Removing Items from inventory went wrong ;(");
            }
        }

        // Add the result to the inventory
        Item item = recipe.craftingResult as Item;
        player.inventory.AddItem(item.itemBase, 1);

        player.inventory.ShowContent();

        return true;
    }

    protected virtual void InstantiateCrafter()
    {
        CrafterObject = GameObject.Instantiate(CrafterPrefab);

        EventTrigger crafterTriggers = CrafterObject.GetComponent<EventTrigger>();
        EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
        pointerClickEntry.eventID = EventTriggerType.PointerClick;
        pointerClickEntry.callback.AddListener((data) => OpenCrafterMenu((PointerEventData)data));
        crafterTriggers.triggers.Add(pointerClickEntry);
    }

    protected virtual void OpenCrafterMenu(PointerEventData _pointerData)
    {
        CrafterMenu crafterMenu = new CrafterMenu(player.menuStateMachine, gameManager, this);
        player.menuStateMachine.SetState(crafterMenu);
    }
}
