using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/SubnauticaBuilder")]
public class SubnauticaBuilder : SceneBuilder
{
    public override void BuildScene(GameManager _gameManager) {
        Player player = new Player(_gameManager);
        new DefaultCrafter(_gameManager, player);
        new ItemEnhancer(_gameManager, player);

        new ItemSource(_gameManager, 
                        (sItemBase) _gameManager.scriptableObjectLibrary.GetScriptableObject("Wood"), 
                        _gameManager.prefabLibrary.GetPrefab("WoodSource"));
    }
}
