using UnityEngine;

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
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
        inventoryUI.gameObject.SetActive(true);
    }

    public override void DisableState()
    {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        inventoryUI.gameObject.SetActive(false);
    }
}
