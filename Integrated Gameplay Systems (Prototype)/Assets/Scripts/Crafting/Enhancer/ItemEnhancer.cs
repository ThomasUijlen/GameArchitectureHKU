using System.Collections.Generic;
using UnityEngine;

public class ItemEnhancer : ACrafter
{
    public override sRecipeList recipes => (sRecipeList) gameManager.scriptableObjectLibrary.GetScriptableObject("EnhancerRecipeList");
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("ClassItemEnhancer");
    protected override string CrafterTag => "Enhancer";


    public ItemEnhancer(GameManager _gameManager, Vector3 _position, Quaternion _rotation) : base(_gameManager, _position, _rotation)
    {
    }
}
