using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtensions
{
    public static bool ContainsItem(this Dictionary<sItemBase, int> _items, sItemBase _item)
    {
        foreach (sItemBase storedItem in _items.Keys)
        {
            if (storedItem.Equals(_item)) return true;
        }
        return false;
    }

    public static int GetItemAmount(this Dictionary<sItemBase, int> _items, sItemBase _item)
    {
        foreach (sItemBase storedItem in _items.Keys)
        {
            if (storedItem.Equals(_item)) return _items[storedItem];
        }
        return -1;
    }
}
