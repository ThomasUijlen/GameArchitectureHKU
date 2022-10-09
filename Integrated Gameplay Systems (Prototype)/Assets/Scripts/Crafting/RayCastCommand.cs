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
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f))
        {
            Debug.Log("Raycast!!");
            onRaycastHit?.Invoke(hit);
        }
    }
}
