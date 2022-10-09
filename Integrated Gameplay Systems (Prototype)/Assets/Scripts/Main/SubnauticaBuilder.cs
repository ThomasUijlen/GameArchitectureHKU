using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/SubnauticaBuilder")]
public class SubnauticaBuilder : SceneBuilder
{
    public override void BuildScene(GameManager _gameManager) {
        CameraRaycastCommand.ClearEvent();

        Player player = new Player(_gameManager);

        new ItemSource(_gameManager, 
                        (sItemBase) _gameManager.scriptableObjectLibrary.GetScriptableObject("Wood"), 
                        _gameManager.prefabLibrary.GetPrefab("WoodSource"),
                        new Vector3(-10, -1.5f, -3));

        new ItemSource(_gameManager,
                        (sItemBase)_gameManager.scriptableObjectLibrary.GetScriptableObject("Metal"),
                        _gameManager.prefabLibrary.GetPrefab("MetalSource"),
                        new Vector3(-10, -1.5f, 3));
    }
}
