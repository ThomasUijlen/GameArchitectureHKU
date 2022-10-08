public class Item : ICraftingResult
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
}
