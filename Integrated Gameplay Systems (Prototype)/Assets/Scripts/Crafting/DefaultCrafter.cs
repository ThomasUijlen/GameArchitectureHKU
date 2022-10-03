using UnityEngine;

public class DefaultCrafter : BasicObject, ICrafter
{
    private GameManager gameManager;
    private CrafterMenu menu;

    sRecipeList ICrafter.recipes
    {
        get
        {
            sRecipeList list = new sRecipeList();
            return list;
        }
    }

    public DefaultCrafter(GameManager _gameManager) : base(_gameManager)
    {
        gameManager = _gameManager;

        InstantiateMenu(_gameManager);
    }

    private void InstantiateMenu(GameManager _gameManager)
    {
        if (_gameManager.prefabLibrary.HasPrefab("CrafterMenuUI"))
        {
            Canvas menuUI = _gameManager.prefabLibrary.InstantiatePrefab("CrafterMenuUI").GetComponent<Canvas>();
            menu = new CrafterMenu(_gameManager, menuUI, this);
        }
        else Debug.Log("RIP");
    }
}
