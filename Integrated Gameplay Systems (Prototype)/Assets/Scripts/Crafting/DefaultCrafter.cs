using System.Collections.Generic;
using UnityEngine;

public class DefaultCrafter : ACrafter
{
    public override sRecipeList recipes => (sRecipeList) gameManager.scriptableObjectLibrary.GetScriptableObject("DefaultCrafterList");
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("ClassDefaultCrafter");
    protected override string CrafterTag => "Crafter";

    public DefaultCrafter(GameManager _gameManager, Vector3 _position, Quaternion _rotation) : base(_gameManager, _position, _rotation) { }
}
