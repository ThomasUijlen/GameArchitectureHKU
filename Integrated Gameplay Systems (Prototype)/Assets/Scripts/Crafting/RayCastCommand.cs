using System;
using UnityEngine;

public class CameraRaycastCommand : ICommand
{
    public static event Action<RaycastHit> onRaycastHit;
    private GameObject playerCamera;

    public CameraRaycastCommand(GameManager _gameManager)
    {
        playerCamera = (GameObject) _gameManager.GetObjectWithTag("Camera");
    }

    public void Execute()
    {
        int layerMask = LayerMask.GetMask("Terrain", "Structure", "Interactable");
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f))
        {
            onRaycastHit?.Invoke(hit);
        }
    }
}
