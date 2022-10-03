using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class sItem : ScriptableObject, ICraftingResult
{
    public string name;
    public Image sprite;

    public sItem() { }

    public sItem(string _name, Image _sprite = null)
    {
        name = _name;
        sprite = _sprite;
    }

    public override bool Equals(object other)
    {
        if (other is sItem)
        {
            return name == ((sItem)other).name;
        }
        return false;
    }
}
