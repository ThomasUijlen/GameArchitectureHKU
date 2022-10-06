using System.Collections.Generic;
using UnityEngine;

// TODO: For testing it is a BasicObject, otherwise it should be an 
public class Inventory : BasicObject
{
    private const int capacity = 20;

    private Dictionary<sItem, int> items = new Dictionary<sItem, int>();
    private int totalItemCount;

    public Inventory(GameManager _gameManager) : base(_gameManager)
    {
        ServiceLocator.RegisterService<Inventory>(this);
        // For testing
        AddItem(ItemLibrary.Wood, 10);
        AddItem(ItemLibrary.Stone, 4);
    }

    public bool AddItem(sItem _item, int _amount)
    {
        if (_amount + totalItemCount >= capacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        if (items.ContainsKey(_item))
        {
            items[_item] += _amount;
        }
        else
        {
            items.Add(_item, _amount);
        }

        totalItemCount += _amount;
        return true;
    }

    public bool RemoveItem(sItem _item, int _amount)
    {
        if (items.ContainsKey(_item))
        {
            items[_item] -= _amount;
            totalItemCount -= _amount;
            if (items[_item] <= 0)
            {
                items.Remove(_item);
            }
            return true;
        }

        return false;
    }

    public bool HasItems(sItem _item, int _amount)
    {
        return (items.ContainsKey(_item) && items[_item] >= _amount);
    }

    public void ShowContent()
    {
        foreach (sItem item in items.Keys)
        {
            Debug.Log($"[INVENTORY] Item: {item.name}, Amount: {items[item]}");
        }
    }

    public Dictionary<sItem, int> GetItems()
    {
        return items;
    }

    /*private int CalculateTotalCount()
    {
        int result = 0;
        foreach(sItem item in items.Keys)
        {
            int amount = items[item];
            result += amount;
        }
        return result;
    }*/
}
