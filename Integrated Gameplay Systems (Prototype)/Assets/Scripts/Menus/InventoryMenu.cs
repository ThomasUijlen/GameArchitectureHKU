using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : Menu
{
    private Inventory playerInventory;
    private GameObject inventoryUI;
    private OpenMenuCommand backCommand;

    public InventoryMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager)
    {
        InstantiateUI();
        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);
    }

    public override void EnableState()
    {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);

        playerInventory = gameManager.GetObjectWithTag("Inventory") as Inventory;
        ShowInventory();
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        GameObject.Destroy(inventoryUI);
    }

    private void InstantiateUI()
    {
        if (inventoryUI == null)
        {
            inventoryUI = gameManager.prefabLibrary.InstantiatePrefab("InventoryUI");
        }
    }

    public void ShowInventory()
    {
        Text inventoryContent = GameObject.Find("Result Description").GetComponent<Text>();

        string text = "";

        var items = playerInventory.itemList;
        foreach (Item item in items)
        {
            text += $"{item.name}, Value: {item.goldValue}\n";
        }

        inventoryContent.text = text;
    }
}
