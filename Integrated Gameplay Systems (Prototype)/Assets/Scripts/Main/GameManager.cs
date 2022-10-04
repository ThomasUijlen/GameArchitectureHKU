using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PrefabLibrary prefabLibrary;
    public SceneBuilder sceneBuilder;
    public InputManager inputManager;

    private List<BasicObject> objects = new List<BasicObject>();

    private void Awake()
    {
        prefabLibrary.PrepareLibrary();
    }

    private void Start() {
        inputManager = new InputManager(this);
        if(sceneBuilder != null) sceneBuilder.BuildScene(this);

        // Crafter Things\
        Inventory inventory = new Inventory(this);
        RegisterBasicObject(inventory);
        RegisterBasicObject(new DefaultCrafter(this, inventory));
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
