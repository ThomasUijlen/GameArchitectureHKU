using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/ScriptableObjectLibrary")]
public class ScriptableObjectLibrary : ScriptableObject
{
    [System.Serializable]
    public struct ScriptableObjectReference
    {
        public string name;
        public ScriptableObject scriptableObject;
    }

    public List<ScriptableObjectReference> scriptableObjects = new List<ScriptableObjectReference>();
    private Dictionary<string, ScriptableObject> scriptableObjectLibrary = new Dictionary<string, ScriptableObject>();

    public void PrepareLibrary()
    {
        foreach (ScriptableObjectReference reference in scriptableObjects)
        {
            scriptableObjectLibrary.Add(reference.name, reference.scriptableObject);
        }
        AddHardCodedScriptableObjects();
    }

    public bool HasScriptableObject(string _name)
    {
        return scriptableObjectLibrary.ContainsKey(_name);
    }

    public ScriptableObject GetScriptableObject(string _name)
    {
        return scriptableObjectLibrary[_name];
    }

    private void AddHardCodedScriptableObjects()
    {
        List<ItemAmountPair> ingredients = new List<ItemAmountPair>
        {
            new ItemAmountPair(GetScriptableObject("Wood") as sItemBase, 2)
        };

        sRecipe stickRecipe = new sRecipe()
        {
            ingredients = ingredients,
            craftingResult = new Item(GetScriptableObject("Stick") as sItemBase)
        };

        List<ItemAmountPair> ingredients2 = new List<ItemAmountPair>
        {
            new ItemAmountPair(GetScriptableObject("Stick") as sItemBase, 2),
            new ItemAmountPair(GetScriptableObject("Stone") as sItemBase, 2)
        };

        sRecipe toolRecipe = new sRecipe()
        {
            ingredients = ingredients2,
            craftingResult = new Item(GetScriptableObject("Tool") as sItemBase)
        };

        List<ItemAmountPair> ingredients3 = new List<ItemAmountPair>
        {
            new ItemAmountPair(GetScriptableObject("Wood") as sItemBase, 1),
            new ItemAmountPair(GetScriptableObject("Stone") as sItemBase, 1)
        };

        Item EnhancedWood = new GoldValueEnhancer(40).Enhance(new Item(GetScriptableObject("Wood") as sItemBase));

        sRecipe enhancedWoodRecipe = new sRecipe()
        {
            ingredients = ingredients3,
            craftingResult = EnhancedWood,
        };

        scriptableObjectLibrary.Add("stickRecipe", stickRecipe);
        scriptableObjectLibrary.Add("toolRecipe", toolRecipe);
        scriptableObjectLibrary.Add("enhancedWoodRecipe", enhancedWoodRecipe);
    }
}
