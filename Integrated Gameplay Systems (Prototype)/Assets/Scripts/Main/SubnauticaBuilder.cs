using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomAssets/SubnauticaBuilder")]
public class SubnauticaBuilder : SceneBuilder
{
    public override void BuildScene(GameManager _gameManager) {
        Player player = new Player(_gameManager);
        //new DefaultCrafter(_gameManager, player);

        _gameManager.RegisterTag("Camera", GameObject.Find("Main Camera"));
    }
}
