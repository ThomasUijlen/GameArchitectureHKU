using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class sItemBase : ScriptableObject, ICraftingResult
{
    public string name;
    public Image sprite;
    public int baseGoldValue;

    public sItemBase() { }

    public sItemBase(string _name, Image _sprite = null)
    {
        name = _name;
        sprite = _sprite;
    }

    public override bool Equals(object other)
    {
        if (other is sItemBase)
        {
            return name == ((sItemBase)other).name;
        }
        return false;
    }
}