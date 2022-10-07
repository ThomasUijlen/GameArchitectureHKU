[System.Serializable]
public class ItemAmountPair
{
    public sItemBase itemBase;
    public int amount;

    public ItemAmountPair(sItemBase _itemBase, int _amount)
    {
        itemBase = _itemBase;
        amount = _amount;
    }
}
