using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class sItemBase : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public int baseGoldValue;

    public sItemBase() { }

    public sItemBase(string _name, Sprite _sprite = null)
    {
        name = _name;
        sprite = _sprite;
    }

    public override bool Equals(object _other)
    {
        if (_other is sItemBase)
        {
            return name == ((sItemBase)_other).name;
        }
        return false;
    }
}
