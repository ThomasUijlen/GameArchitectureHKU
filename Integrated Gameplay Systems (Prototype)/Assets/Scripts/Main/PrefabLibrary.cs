using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/PrefabLibrary")]
public class PrefabLibrary : ScriptableObject
{
    [System.Serializable]
    public struct PrefabReference
    {
        public string name;
        public GameObject prefab;
    }
    
    public List<PrefabReference> prefabs = new List<PrefabReference>();

    private Dictionary<string, GameObject> prefabLibrary = new Dictionary<string, GameObject>();

    public void PrepareLibrary()
    {
        prefabLibrary.Clear();

        foreach (PrefabReference reference in prefabs)
        {
            prefabLibrary.Add(reference.name, reference.prefab);
        }
    }

    public bool HasPrefab(string _name)
    {
        return prefabLibrary.ContainsKey(_name);
    }

    public GameObject GetPrefab(string _name)
    {
        return prefabLibrary[_name];
    }

    public GameObject InstantiatePrefab(string _name)
    {
        return GameObject.Instantiate(prefabLibrary[_name]);
    }
}
