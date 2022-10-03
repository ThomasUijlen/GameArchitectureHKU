using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BasicObject
{
    private Dictionary<KeyCode, List<ICommand>> keyBindings = new Dictionary<KeyCode, List<ICommand>>();

    public InputManager(GameManager _gameManager) : base(_gameManager) {}

    public override void Update()
    {
        foreach(KeyCode key in keyBindings.Keys) {
            if(Input.GetKeyDown(key)) {
                foreach(ICommand command in keyBindings[key]) {
                    command.Execute();
                }
            }
        }
    }

    public void RegisterKeyBinding(KeyCode _keyCode, ICommand _command) {
        if(!keyBindings.ContainsKey(_keyCode)) keyBindings.Add(_keyCode, new List<ICommand>());
        keyBindings[_keyCode].Add(_command);
    }

    public void DeregisterKeyBinding(KeyCode _keyCode, ICommand _command) {
        keyBindings[_keyCode].Remove(_command);
    }
}
