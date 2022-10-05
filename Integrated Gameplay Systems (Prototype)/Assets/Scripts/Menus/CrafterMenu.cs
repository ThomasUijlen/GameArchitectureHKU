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

    private GameObject menuCanvas;
    private GameObject recipeInfo;
    private Text resultNameText;
    private Text ingredientInfoText;
    private IStateMachine stateMachine;

    public CrafterMenu(IStateMachine _stateMachine, GameManager _gameManager, ICrafter _crafter) : base(_stateMachine, _gameManager)
    {
        stateMachine = _stateMachine;
        crafter = _crafter;
        menuCanvas = _gameManager.prefabLibrary.InstantiatePrefab("CrafterMenuUI");

        AddRecipesToScrollView();
    }

    public override void DisableState()
    {
        menuCanvas.gameObject.SetActive(false);
    }

    public override void EnableState()
    {
        menuCanvas.gameObject.SetActive(true);
    }

    private void AddRecipesToScrollView()
    {
        sRecipeList recipeList = crafter.recipes;

        GameObject scrollViewContent = GameObject.Find("Content");      // BAD 
        GameObject recipeButtonPrefab = gameManager.prefabLibrary.GetPrefab("RecipeButton");    // Should add an overload for InstantiatePrefab in prefablibrary

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

        // From Unity Documentation: EventTrigger
        EventTrigger buttonTriggers = button.GetComponent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((data) => UpdateIngedientsInfo((PointerEventData)data));
        buttonTriggers.triggers.Add(hoverEntry);
    }

    private static void DisplayRecipeUI(GameObject _recipeButton, sRecipe _recipe)
    {
        // If more texts, I should use tags to distinguish them?
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
