using System.Collections.Generic;
using UnityEngine;

public class UIList
{
    public List<GameObject> elements = new List<GameObject>();

    private GameObject prefab;
    private GameObject parent;

    // Layout \\
    private float elementOffset;
    private Vector3 lastButtonPos;

    public UIList(GameObject _prefab, GameObject _parent, float _elementOffset)
    {
        prefab = _prefab;
        parent = _parent;
        elementOffset = _elementOffset;
    }

    public GameObject AddElement()
    {
        GameObject newInstance = GameObject.Instantiate(prefab, parent.transform);
        elements.Add(newInstance);

        // Positioning
        if (lastButtonPos != default)
        {
            lastButtonPos = lastButtonPos - Vector3.up * elementOffset;
            newInstance.transform.position = lastButtonPos;
        }
        else
        {
            lastButtonPos = newInstance.transform.position;
        }

        return newInstance;
    }


}
