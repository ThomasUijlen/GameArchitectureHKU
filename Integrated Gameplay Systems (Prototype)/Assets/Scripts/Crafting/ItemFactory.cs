using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static Item CreateItem(sItemBase itemBase)
    {
        return new Item(itemBase);
    }
}
