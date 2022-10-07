using UnityEngine;

public class GoldValueEnhancer : ItemDecorator
{
    private int amount;

    public GoldValueEnhancer(int _amount)
    {
        amount = _amount;
    }

    public override Item Enhance(Item _item)
    {
        float oldValue = _item.goldValue;
        _item.goldValue += amount;
        Debug.Log($"Enhanced Gold Value from {oldValue} to {_item.goldValue}");
        return _item;
    }
}
