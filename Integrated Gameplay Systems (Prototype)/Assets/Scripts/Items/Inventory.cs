using System.Collections.Generic;
using UnityEngine;

public class Inventory : BasicObject
{
    private const int capacity = 20;
    
    private Dictionary<sItemBase, int> items = new Dictionary<sItemBase, int>();
    private int totalItemCount;

    public Inventory(GameManager _gameManager) : base(_gameManager)
    {
        ServiceLocator.RegisterService<Inventory>(this);
        // For testing
        //AddItem(new Item(new sItemBase("Wood"), 50), 10);
        //AddItem(new Item(new sItemBase("Stone"), 20), 4);
        AddItem(gameManager.scriptableObjectLibrary.GetScriptableObject("Wood") as sItemBase, 10);
        AddItem(gameManager.scriptableObjectLibrary.GetScriptableObject("Stone") as sItemBase, 4);
    }

    /*public bool AddItem(Item _item, int _amount)
    {
        if (_item == null) return false;

        if (_amount + totalItemCount >= capacity)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        if (items.ContainsKey(_item.itemBase))
        {
            items[_item.itemBase] += _amount;
        }
        else
        {
            items.Add(_item.itemBase, _amount);
        }

        totalItemCount += _amount;
        return true;
    }*/

    public bool AddItem(sItemBase _item, int _amount)
    {
        if (_item == null) return false;

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

    public bool RemoveItem(sItemBase _item, int _amount)
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

    public bool HasItems(sItemBase _item, int _amount)
    {
        return (items.ContainsKey(_item) && items[_item] >= _amount);
    }

    public void ShowContent()
    {
        foreach (sItemBase item in items.Keys)
        {
            Debug.Log($"[INVENTORY] Item: {item.name}, Amount: {items[item]}");
        }
    }

    public Dictionary<sItemBase, int> GetItems()
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
