using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PrefabLibrary prefabLibrary;
    public SceneBuilder sceneBuilder;
    public InputManager inputManager;

    private List<BasicObject> objects = new List<BasicObject>();
    private Dictionary<string, object> tagList = new Dictionary<string, object>();

    private void Awake()
    {
        prefabLibrary.PrepareLibrary();
    }

    private void Start() {
        inputManager = new InputManager(this);
        if(sceneBuilder != null) sceneBuilder.BuildScene(this);
    }

    public void Update()
    {
        foreach(BasicObject obj in objects) obj.Update();
    }

    public void FixedUpdate()
    {
        foreach(BasicObject obj in objects) obj.FixedUpdate();
    }

    public void RegisterBasicObject(BasicObject _object) {
        objects.Add(_object);
    }

    public void DeregisterBasicObject(BasicObject _object) {
        if(objects.Contains(_object)) objects.Remove(_object);
    }

    public void RegisterTag(string tag, object obj) {
        if(tagList.ContainsKey(tag)) {
            tagList[tag] = obj;
        } else {
            tagList.Add(tag, obj);
        }
    }

    public void DeregisterTag(string tag) {
        if(tagList.ContainsKey(tag)) {
            tagList.Remove(tag);
        }
    }

    public object GetObjectWithTag(string tag) {
        if(tagList.ContainsKey(tag)) {
            return tagList[tag];
        }
        return null;
    }
}
