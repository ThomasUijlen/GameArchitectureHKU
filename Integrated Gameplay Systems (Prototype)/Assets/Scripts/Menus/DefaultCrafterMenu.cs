using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrafterMenu : Menu
{
    private Dictionary<Button, sRecipe> recipeButtons = new Dictionary<Button, sRecipe>();
    // Misschien later heeft elk menu een Canvas met UI
    private GameManager gameManager;
    private ICrafter crafter;

    private Canvas menuCanvas;
    private GameObject recipeInfo;
    private Text resultNameText;

    // Layout \\
    private float buttonOffset = 85f;
    private Vector3 lastButtonPos;
    

    public CrafterMenu(GameManager _gameManager, Canvas _menuCanvas, ICrafter _crafter) : base(_gameManager)
    {
        gameManager = _gameManager;
        menuCanvas = _menuCanvas;
        crafter = _crafter;

        AddRecipesToScrollView();
        //menuCanvas = _menuCanvas;
    }

    public override void DisableMenu()
    {
        menuCanvas.gameObject.SetActive(false);
    }

    public override void EnableMenu()
    {
        menuCanvas.gameObject.SetActive(true);
    }

    private void AddRecipesToScrollView()
    {
        sRecipeList recipeList = crafter.recipes;

        GameObject scrollViewContent = GameObject.Find("Content");      // BAD 
        // Should add an overload for InstantiatePrefab in prefablibrary
        GameObject recipeButtonPrefab = gameManager.prefabLibrary.GetPrefab("RecipeButton");

        recipeInfo = GameObject.Find("Recipe Info");
        resultNameText = recipeInfo.GetComponentInChildren<Text>();

        foreach (sRecipe recipe in recipeList.recipes)
        {
            CreateRecipeButton(scrollViewContent, recipeButtonPrefab, recipe);
        }
    }

    private void CreateRecipeButton(GameObject _scrollViewContent, GameObject _recipeButtonPrefab, sRecipe _recipe)
    {
        // Object pool? Probably not but maybe
        GameObject recipeButton = GameObject.Instantiate(_recipeButtonPrefab, _scrollViewContent.transform);

        // If more texts, I should use tags to distinguish them?
        Text recipeNameText = recipeButton.GetComponentInChildren<Text>();
        if (recipeNameText != null)
        {
            recipeNameText.text = _recipe.craftingResult.name;
        }

        Button button = recipeButton.GetComponent<Button>();
        AddButtonCraftEvent(button, _recipe);

        recipeButtons.Add(button, _recipe);

        // From Unity Documentation: EventTrigger
        EventTrigger buttonTriggers = button.GetComponent<EventTrigger>();
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((data) => UpdateIngedientsInfo((PointerEventData) data));
        buttonTriggers.triggers.Add(hoverEntry);

        // Positioning
        if (lastButtonPos != default)
        {
            lastButtonPos = lastButtonPos - Vector3.up * buttonOffset;
            recipeButton.transform.position = lastButtonPos;
        }
        else
        {
            lastButtonPos = recipeButton.transform.position;
        }
    }

    private void UpdateIngedientsInfo(PointerEventData data)
    {
        if (data == null) return;
        //Debug.Log($"HOVERED over {data.pointerEnter.gameObject.name}");

        Button button = data.pointerEnter.GetComponent<Button>();
        if (recipeButtons.ContainsKey(button))
        {
            sRecipe recipe = recipeButtons[button];
            
            resultNameText.text = recipe.craftingResult.name;
        }
    }

    private void AddButtonCraftEvent(Button _button, sRecipe _recipe)
    {
        _button.onClick.AddListener(() => crafter.Craft(_recipe));
    }

}
