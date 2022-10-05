[System.Serializable]
public class ItemAmountPair
{
    public sItem item;
    public int amount;

    public ItemAmountPair(sItem _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
}
