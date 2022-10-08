using System.Collections.Generic;
using UnityEngine;

public class ItemEnhancer : ACrafter
{
    public override sRecipeList recipes => (sRecipeList) gameManager.scriptableObjectLibrary.GetScriptableObject("EnhancerRecipeList");
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Enhancer");

    public ItemEnhancer(GameManager _gameManager, Player _player) : base(_gameManager, _player)
    {
        CrafterObject.transform.position = new Vector3(-3, 0, 0);
    }
}
