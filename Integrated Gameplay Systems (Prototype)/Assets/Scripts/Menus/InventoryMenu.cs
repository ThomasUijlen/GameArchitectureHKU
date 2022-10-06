using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : Menu
{
    private Inventory playerInventory;
    private GameObject inventoryUI;
    private OpenMenuCommand backCommand;

    public InventoryMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager)
    {
        inventoryUI = gameManager.prefabLibrary.InstantiatePrefab("InventoryUI");
        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);

    }

    public override void EnableState()
    {
        playerInventory = ServiceLocator.GetService<Inventory>();
        ShowInventory();

        playerInventory.ShowContent();
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
        inventoryUI.gameObject.SetActive(true);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        inventoryUI.gameObject.SetActive(false);
    }

    public void ShowInventory()
    {
        Text inventoryContent = GameObject.Find("Result Description").GetComponent<Text>();

        string text = "";

        var items = playerInventory.GetItems();
        foreach (sItem item in items.Keys)
        {
            text += $"{item.name} x{items[item]}, Value: {item.goldValue}\n";
        }

        inventoryContent.text = text;
    }
}
