using UnityEngine;

public class NoMenu : Menu
{
    private OpenMenuCommand buildMenuCommand;
    private OpenMenuCommand InventoryMenuCommand;
    private CameraRaycastCommand raycastCommand;

    public NoMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        buildMenuCommand = new OpenMenuCommand(typeof(BuildMenu), _stateMachine, _gameManager);
        InventoryMenuCommand = new OpenMenuCommand(typeof(InventoryMenu), _stateMachine, _gameManager);
        raycastCommand = new CameraRaycastCommand(_gameManager);
        allowMovement = true;
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.B, buildMenuCommand);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Tab, InventoryMenuCommand);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.I, InventoryMenuCommand);
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Mouse0, raycastCommand, InputManager.INPUT_MODE.SINGLE_PRESSED);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.B, buildMenuCommand);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Tab, InventoryMenuCommand);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.I, InventoryMenuCommand);
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Mouse0, raycastCommand);

        Cursor.lockState = CursorLockMode.None;
    }
}
