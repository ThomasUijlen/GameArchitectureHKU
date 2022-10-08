using System.Collections.Generic;
using UnityEngine;

public class Inventory : BasicObject
{
    public List<Item> itemList { get; private set; } = new List<Item>();
    public Dictionary<sItemBase, int> itemBaseList { get; private set; } = new Dictionary<sItemBase, int>();

    private const int capacity = 20;
    private int totalItemCount;

    public Inventory(GameManager _gameManager) : base(_gameManager)
    {
        // For testing
        sItemBase sWood = gameManager.scriptableObjectLibrary.GetScriptableObject("Wood") as sItemBase;
        sItemBase sStone = gameManager.scriptableObjectLibrary.GetScriptableObject("Stone") as sItemBase;

        AddItemBase(sWood, 10);
        AddItemBase(sStone, 4);
    }

    // Adds a specific Item to the inventory. Used when adding a crafted item.
    public bool AddItem(Item _item)
    {
        if (_item == null || totalItemCount == capacity) return false;

        itemList.Add(_item);
        AddItemBaseToDictionary(_item.itemBase, 1);
        return true;
    }

    // Adds an item type to the inventory with amount of _amount and generates a new item instance for each amount.
    public bool AddItemBase(sItemBase _itemBase, int _amount)
    {
        if (_itemBase == null) return false;

        if (_amount + totalItemCount >= capacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        AddItemBaseToDictionary(_itemBase, _amount);
        for (int i = 0; i < _amount; i++)
        {
            itemList.Add(new Item(_itemBase));
        }

        return true;
    }

    // Removes a specific item from the inventory.
    public bool RemoveItem(Item _item)
    {
        if (itemBaseList.ContainsKey(_item.itemBase))
        {
            itemList.Remove(_item);

            sItemBase itemType = _item.itemBase;
            itemBaseList[itemType] -= 1;
            totalItemCount -= 1;
            if (itemBaseList[itemType] <= 0)
            {
                itemBaseList.Remove(itemType);
            }
            return true;
        }

        return false;
    }

    // Removes an itemType from the inventory _amount of times. Then removes the item instance of that type with the lowest gold value.
    public bool RemoveItemBase(sItemBase _itemBase, int _amount)
    {
        if (itemBaseList.ContainsKey(_itemBase))
        {
            itemBaseList[_itemBase] -= _amount;
            totalItemCount -= _amount;
            if (itemBaseList[_itemBase] <= 0)
            {
                itemBaseList.Remove(_itemBase);
            }

            for (int i = 0; i < _amount; i++)
            {
                itemList.Remove(FindItemWithBaseLowest(_itemBase));
            }

            return true;
        }

        return false;
    }

    public bool HasItems(sItemBase _item, int _amount)
    {
        return (itemBaseList.ContainsKey(_item) && itemBaseList[_item] >= _amount);
    }

    public void ShowContent()
    {
        foreach (sItemBase item in itemBaseList.Keys)
        {
            Debug.Log($"[INVENTORY] Item: {item.name}, Amount: {itemBaseList[item]}");
        }
    }

    // Finds an Item with the sItemBase of _itemBase and returns the item with the lowest value.
    public Item FindItemWithBaseLowest(sItemBase _itemBase)
    {
        Item lowestValueItem = null;

        foreach (Item item in itemList)
        {
            if (item.itemBase.Equals(_itemBase))
            {
                if (lowestValueItem == null)
                {
                    lowestValueItem = item;
                }
                else if (item.goldValue < lowestValueItem.goldValue) {
                    lowestValueItem = item;
                }
            }
        }

        return lowestValueItem;
    }

    private void AddItemBaseToDictionary(sItemBase _itemBase, int _amount)
    {
        if (itemBaseList.ContainsKey(_itemBase))
        {
            itemBaseList[_itemBase] += _amount;
        }
        else
        {
            itemBaseList.Add(_itemBase, _amount);
        }

        totalItemCount += _amount;
    }
}
