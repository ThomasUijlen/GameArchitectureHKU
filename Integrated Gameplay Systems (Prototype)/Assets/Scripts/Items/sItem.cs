using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class sItem : ScriptableObject, ICraftingResult
{
    public string name { get; }
    public Image sprite;
    public float goldValue;

    public sItem() { }

    public sItem(string _name, float _goldValue = 0f, Image _sprite = null)
    {
        name = _name;
        goldValue = _goldValue;
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
