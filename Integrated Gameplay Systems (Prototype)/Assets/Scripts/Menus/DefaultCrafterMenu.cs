using UnityEngine;
using UnityEngine.UI;

public class CrafterMenu : Menu
{
    // Misschien later heeft elk menu een Canvas met UI
    private ICrafter crafter;

    private Canvas menuCanvas;
    private Button menuButton;

    public CrafterMenu(GameManager _gameManager, Canvas _menuCanvas, ICrafter _crafter) : base(_gameManager)
    {
        menuCanvas = _menuCanvas;
        menuButton = _menuCanvas.gameObject.GetComponentInChildren<Button>();
        crafter = _crafter;
        AddButtonEvents();
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

    private void AddButtonEvents()
    {
        menuButton.onClick.AddListener(() => crafter.Craft());
    }
}
