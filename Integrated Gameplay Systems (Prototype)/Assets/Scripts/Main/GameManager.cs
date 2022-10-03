using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PrefabLibrary prefabLibrary;
    public SceneBuilder sceneBuilder;

    private List<BasicObject> objects = new List<BasicObject>();

    private void Awake()
    {
        prefabLibrary.PrepareLibrary();
    }

    private void Start() {
        if(sceneBuilder != null) sceneBuilder.BuildScene(this);
        RegisterBasicObject(new InputManager(this));
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
}
