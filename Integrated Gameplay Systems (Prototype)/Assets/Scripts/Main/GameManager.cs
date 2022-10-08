using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PrefabLibrary prefabLibrary;
    public ScriptableObjectLibrary scriptableObjectLibrary;
    public SceneBuilder sceneBuilder;
    public InputManager inputManager;

    private List<BasicObject> newObjects = new List<BasicObject>();
    private List<BasicObject> objects = new List<BasicObject>();
    private List<BasicObject> oldObjects = new List<BasicObject>();
    private Dictionary<string, object> tagList = new Dictionary<string, object>();

    private void Awake()
    {
        prefabLibrary.PrepareLibrary();
        scriptableObjectLibrary?.PrepareLibrary();
    }

    public void Start() {
        inputManager = new InputManager(this);
        if(sceneBuilder != null) sceneBuilder.BuildScene(this);
    }

    public void Update()
    {
        IntegrateNewObjects();
        foreach(BasicObject obj in objects) obj.Update();
        RemoveOldObjects();
    }

    public void FixedUpdate()
    {
        IntegrateNewObjects();
        foreach(BasicObject obj in objects) obj.FixedUpdate();
        RemoveOldObjects();
    }

    private void IntegrateNewObjects() {
        objects.AddRange(newObjects);
        newObjects.Clear();
    }

    private void RemoveOldObjects() {
        foreach(BasicObject obj in oldObjects) objects.Remove(obj);
        oldObjects.Clear();
    }

    public void RegisterBasicObject(BasicObject _object) {
        newObjects.Add(_object);
    }

    public void DeregisterBasicObject(BasicObject _object) {
        oldObjects.Add(_object);
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
