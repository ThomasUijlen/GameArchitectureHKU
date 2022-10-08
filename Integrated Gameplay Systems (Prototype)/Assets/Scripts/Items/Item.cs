using System.Collections.Generic;

public class Item
{
    // Base \\
    public string name => itemBase.name;
    public sItemBase itemBase;

    // Modifiable variables \\
    public int goldValue;

    public Item(sItemBase _itemBase)
    {
        itemBase = _itemBase;
        goldValue = _itemBase.baseGoldValue;
    }

    public Item(sItemBase _itemBase, int _goldValue)
    {
        itemBase = _itemBase;
        goldValue = _goldValue;
    }

    public void ApplyDecorators(List<sEnhancer> enhancers)
    {
        if (enhancers == null) return;

        foreach(sEnhancer enhancer in enhancers)
        {
            ItemDecorator decorator = EnhancerFactory.CreateItemDecorator(enhancer);
            decorator.Enhance(this);
        }
    }
}
