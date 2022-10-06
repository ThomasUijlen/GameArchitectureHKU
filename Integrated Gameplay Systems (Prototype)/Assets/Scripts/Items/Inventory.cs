using System.Collections.Generic;
using UnityEngine;

// TODO: For testing it is a BasicObject, otherwise it should be an 
public class Inventory : BasicObject
{
    private Dictionary<sItem, int> items = new Dictionary<sItem, int>();

    public Inventory(GameManager _gameManager) : base(_gameManager)
    {
        items.Add(ItemLibrary.Wood, 10);
        items.Add(ItemLibrary.Stone, 4);
    }

    public void AddItem(sItem _item, int _amount)
    {
        if (items.ContainsKey(_item))
        {
            items[_item] += _amount;
        }
        else
        {
            items.Add(_item, _amount);
        }
    }

    public bool RemoveItem(sItem _item, int _amount)
    {
        if (items.ContainsKey(_item))
        {
            items[_item] -= _amount;
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
}
