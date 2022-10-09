using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrafterMenu : Menu
{
    // Misschien later heeft elk menu een Canvas met UI
    private Dictionary<Button, sRecipe> recipeButtons = new Dictionary<Button, sRecipe>();
    private ICrafter crafter;
    private OpenMenuCommand backCommand;
    private IStateMachine stateMachine;

    private GameObject menuCanvas;
    private GameObject recipeInfo;
    private Text resultNameText;
    private Text ingredientInfoText;

    public CrafterMenu(IStateMachine _stateMachine, GameManager _gameManager, ICrafter _crafter) : base(_stateMachine, _gameManager)
    {
        stateMachine = _stateMachine;
        crafter = _crafter;
        menuCanvas = _gameManager.prefabLibrary.InstantiatePrefab("CrafterMenuUI");

        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);
        AddRecipesToScrollView();
    }

    public override void EnableState()
    {
        Debug.Log("Crafter Menu");
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        GameObject.Destroy(menuCanvas);
    }

    private void AddRecipesToScrollView()
    {
        sRecipeList recipeList = crafter.recipes;

        GameObject scrollViewContent = GameObject.Find("Content");
        GameObject recipeButtonPrefab = gameManager.prefabLibrary.GetPrefab("RecipeButton");

        recipeInfo = GameObject.Find("Recipe Info");
        resultNameText = recipeInfo.GetComponentInChildren<Text>();
        ingredientInfoText = GameObject.Find("Ingredient Info").GetComponentInChildren<Text>();

        UIList buttonList = new UIList(recipeButtonPrefab, scrollViewContent, 85f);

        foreach (sRecipe recipe in recipeList.recipes)
        {
            GameObject recipeButton = buttonList.AddElement();
            AddRecipeButtonEvents(recipeButton, recipe);
            DisplayRecipeUI(recipeButton, recipe);
        }
    }

    private void AddRecipeButtonEvents(GameObject _recipeButton, sRecipe _recipe)
    {
        Button button = _recipeButton.GetComponent<Button>();
        AddButtonCraftEvent(button, _recipe);

        recipeButtons.Add(button, _recipe);

        EventTriggerDecorator.AddTrigger(_recipeButton, EventTriggerType.PointerEnter, 
                                        (data) => UpdateIngedientsInfo((PointerEventData) data));
    }

    private static void DisplayRecipeUI(GameObject _recipeButton, sRecipe _recipe)
    {
        Text recipeNameText = _recipeButton.GetComponentInChildren<Text>();
        if (recipeNameText != null)
        {
            recipeNameText.text = _recipe.craftingResult.name;
        }
    }

    private void UpdateIngedientsInfo(PointerEventData _pointerData)
    {
        if (_pointerData == null) return;

        Button button = _pointerData.pointerEnter.GetComponent<Button>();
        if (recipeButtons.ContainsKey(button))
        {
            sRecipe recipe = recipeButtons[button];
            
            resultNameText.text = recipe.craftingResult.name;
            ingredientInfoText.text = recipe.IngredientsString();
        }
    }

    private void AddButtonCraftEvent(Button _button, sRecipe _recipe)
    {
        _button.onClick.AddListener(() => crafter.Craft(_recipe));
    }
}
