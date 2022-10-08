using UnityEngine;
using UnityEngine.EventSystems;

// This class is used to give the player resources
public class ItemSource : BasicObject
{
    public string name => $"{itemBase.name} Source";
    private GameObject prefab;
    private GameObject sourceObject;
    private sItemBase itemBase;

    public ItemSource(GameManager _gameManager, sItemBase _itemBase, GameObject _prefab, Vector3 _startingPos = default) : base(_gameManager)
    {
        itemBase = _itemBase;
        prefab = _prefab;
        InstantiateItemSource(_startingPos);
    }

    private void InstantiateItemSource(Vector3 _startingPos = default)
    {
        sourceObject = GameObject.Instantiate(prefab);
        sourceObject.transform.position = _startingPos;

        EventTriggerDecorator.AddTrigger(sourceObject, EventTriggerType.PointerClick,
                                            (data) => OnPlayerInteract((PointerEventData) data));
    }

    private void OnPlayerInteract(PointerEventData data)
    {
        Inventory playerInventory = (Inventory) gameManager.GetObjectWithTag("Inventory");
        playerInventory.AddItemBase(itemBase, 1);
        Debug.Log($"Added {itemBase.name} to the inventory");
    }
}
