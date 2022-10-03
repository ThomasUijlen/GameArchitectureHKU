using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class sItem : ScriptableObject, ICraftingResult
{
    public string name;
    public Image sprite;
}
