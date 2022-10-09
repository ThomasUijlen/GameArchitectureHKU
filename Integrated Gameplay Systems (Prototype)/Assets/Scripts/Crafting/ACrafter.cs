using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ACrafter : BasicObject, ICrafter
{
    public abstract sRecipeList recipes { get; }

    protected abstract GameObject CrafterPrefab { get; }
    protected abstract string CrafterTag { get; }
    protected GameObject CrafterObject;
    protected Player player;

    public ACrafter(GameManager _gameManager, Vector3 _position, Quaternion _rotation) : base(_gameManager)
    {
        player = (Player) gameManager.GetObjectWithTag("Player");
        CameraRaycastCommand.onRaycastHit += CheckRaycastHit;
        InstantiateCrafter(_position, _rotation);
    }

    public virtual bool Craft(sRecipe _recipe)
    {
        // Check if inventory has the correct items
        foreach (ItemAmountPair pair in _recipe.ingredients)
        {
            if (!player.inventory.HasItems(pair.itemBase, pair.amount))
            {
                Debug.Log("Player did not have the right ingredients");
                return false;
            }
        }

        // Remove all the ingredients from the inventory
        foreach (ItemAmountPair pair in _recipe.ingredients)
        {
            if (!player.inventory.RemoveItemBase(pair.itemBase, pair.amount))
            {
                Debug.Log("Removing Items from inventory went wrong");
            }
        }

        Item item = ItemFactory.CreateItem(_recipe.craftingResult);
        item.ApplyDecorators(_recipe.itemDecorators);
        player.inventory.AddItem(item);

        return true;
    }

    public void CheckRaycastHit(RaycastHit _hit)
    {
        if (_hit.collider.tag == CrafterTag)
        {
            OpenCrafterMenu();
        }
    }

    protected virtual void InstantiateCrafter(Vector3 _position, Quaternion _rotation)
    {
        CrafterObject = GameObject.Instantiate(CrafterPrefab, _position, _rotation);
    }

    protected virtual void OpenCrafterMenu(PointerEventData _pointerData = null)
    {
        CrafterMenu crafterMenu = new CrafterMenu(player.menuStateMachine, gameManager, this);
        player.menuStateMachine.SetState(crafterMenu);
    }
}
