using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<sItem, int> items = new Dictionary<sItem, int>();

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
                return true;
            }
        }

        return false;
    }

    public bool HasItems(sItem _item, int _amount)
    {
        return items.ContainsKey(_item) && items[_item] >= _amount;
    }
}
