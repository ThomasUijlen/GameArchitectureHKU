using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramStructure : BasicObject, ICommand
{
    protected const float PLACE_DISTANCE = 10f;
    protected GameObject hologramObject;
    protected GameObject resultStructure;
    protected GameObject playerCamera;
    private GameObject greenMesh;
    private GameObject redMesh;

    public HologramStructure(string _structureName, GameManager _gameManager) : base(_gameManager) {
        hologramObject = _gameManager.prefabLibrary.InstantiatePrefab(_structureName+"Holo");
        resultStructure = _gameManager.prefabLibrary.GetPrefab(_structureName);
        playerCamera = (GameObject) _gameManager.GetObjectWithTag("Camera");

        greenMesh = GameObject.Find("CanPlace");
        redMesh = GameObject.Find("CantPlace");

        gameManager.inputManager.RegisterKeyBinding(KeyCode.Mouse0, this);
    }

    public override void FixedUpdate()
    {
        PositionStructure();
        SetHoloColor();
    }

    public virtual void PositionStructure() {
        Vector3 cameraPosition = playerCamera.transform.position;
        cameraPosition += playerCamera.transform.forward*PLACE_DISTANCE;
        hologramObject.transform.position = cameraPosition;
    }

    public virtual bool CanPlace() {
        return true;
    }

    private void SetHoloColor() {
        bool canPlace = CanPlace();
        greenMesh.SetActive(canPlace);
        redMesh.SetActive(!canPlace);
    }

    public void Execute() {
        TryPlaceStructure();
    }

    public bool TryPlaceStructure() {
        if(CanPlace()) {
            GameObject.Instantiate(resultStructure, hologramObject.transform.position, hologramObject.transform.rotation);
            return true;
        }

        return false;
    }

    public override void Destroy()
    {
        base.Destroy();
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Mouse0, this);
        GameObject.Destroy(hologramObject);
    }
}
