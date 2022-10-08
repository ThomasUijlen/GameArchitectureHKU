using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class is used to give the player resources
public class ItemSource : BasicObject, ICraftingResult
{
    public string name => $"{itemBase.name} Source";
    private GameObject prefab;
    private GameObject sourceObject;
    private sItemBase itemBase;

    public ItemSource(GameManager _gameManager, sItemBase _itemBase, GameObject _prefab) : base(_gameManager)
    {
        itemBase = _itemBase;
        prefab = _prefab;
    }

    public void AddResultToWorld(GameManager _gameManager)
    {
        InstantiateItemSource();
        //sourceObject.transform.position = new Vector3(0, 0, 3);
    }

    private void InstantiateItemSource()
    {
        sourceObject = GameObject.Instantiate(prefab);
        sourceObject.transform.position = new Vector3(3, 0, 0);

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
