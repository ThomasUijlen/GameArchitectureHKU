// Temporary? Scriptable Object in GameManager?
using UnityEngine;

public static class ItemLibrary
{
    public static sItemBase Wood  {
        get
        {
            sItemBase item = (sItemBase)ScriptableObject.CreateInstance(typeof(sItemBase));
            item.name = "Wood";
            return item;
        }
    }

    public static sItemBase Stick = new sItemBase("Stick");
    public static sItemBase Stone = new sItemBase("Stone");
    public static sItemBase Tool = new sItemBase("Tool");
}
