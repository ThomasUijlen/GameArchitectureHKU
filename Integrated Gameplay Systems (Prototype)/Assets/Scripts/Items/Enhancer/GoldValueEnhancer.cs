using UnityEngine;

public class GoldValueEnhancer : ItemDecorator
{
    private float amount;

    public GoldValueEnhancer(float _amount)
    {
        amount = _amount;
    }

    public override sItem Enhance(sItem item)
    {
        float oldValue = item.goldValue;
        item.goldValue += amount;
        Debug.Log($"Enhanced Gold Value from {oldValue} to {item.goldValue}");
        return item;
    }
}
