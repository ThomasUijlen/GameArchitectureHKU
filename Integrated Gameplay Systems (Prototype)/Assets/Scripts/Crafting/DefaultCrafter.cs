using System.Collections.Generic;
using UnityEngine;

public class DefaultCrafter : ACrafter
{
    public override sRecipeList recipes => (sRecipeList) gameManager.scriptableObjectLibrary.GetScriptableObject("DefaultCrafterList");
    protected override GameObject CrafterPrefab => gameManager.prefabLibrary.GetPrefab("Crafter");
    protected override string CrafterTag => "Crafter";

    public DefaultCrafter(GameManager _gameManager, Player _player) : base(_gameManager, _player) { }
}
