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
        scriptableObjectLibrary.Clear();

        foreach (ScriptableObjectReference reference in scriptableObjects)
        {
            scriptableObjectLibrary.Add(reference.name, reference.scriptableObject);
        }
    }

    public bool HasScriptableObject(string _name)
    {
        return scriptableObjectLibrary.ContainsKey(_name);
    }

    public ScriptableObject GetScriptableObject(string _name)
    {
        return scriptableObjectLibrary[_name];
    }
}
