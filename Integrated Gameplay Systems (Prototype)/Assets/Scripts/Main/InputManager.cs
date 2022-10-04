using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BasicObject
{
    private Dictionary<KeyCode, List<ICommand>> keyBindings = new Dictionary<KeyCode, List<ICommand>>();
    private Dictionary<KeyCode, List<ICommand>> newBindings = new Dictionary<KeyCode, List<ICommand>>();
    private Dictionary<KeyCode, List<ICommand>> oldBindings = new Dictionary<KeyCode, List<ICommand>>();

    public InputManager(GameManager _gameManager) : base(_gameManager) {}

    public override void Update()
    {
        IntegrateNewKeys();
        RemoveOldKeys(); 
        HandleInputs();       
    }

    private void HandleInputs() {
        foreach(KeyCode key in keyBindings.Keys) {
            if(Input.GetKeyDown(key)) {
                foreach(ICommand command in keyBindings[key]) {
                    command.Execute();
                }
            }
        }
    }

    private void IntegrateNewKeys() {
        foreach(KeyCode key in newBindings.Keys) {
            if(!keyBindings.ContainsKey(key)) keyBindings.Add(key, new List<ICommand>());
            keyBindings[key].AddRange(newBindings[key]);
        }
        newBindings.Clear();
    }

    private void RemoveOldKeys() {
        foreach(KeyCode key in oldBindings.Keys) {
            foreach(ICommand command in oldBindings[key]) {
                keyBindings[key].Remove(command);
            }
        }
        oldBindings.Clear();
    }

    public void RegisterKeyBinding(KeyCode _keyCode, ICommand _command) {
        if(!newBindings.ContainsKey(_keyCode)) newBindings.Add(_keyCode, new List<ICommand>());
        newBindings[_keyCode].Add(_command);
    }

    public void DeregisterKeyBinding(KeyCode _keyCode, ICommand _command) {
        if(!oldBindings.ContainsKey(_keyCode)) oldBindings.Add(_keyCode, new List<ICommand>());
        oldBindings[_keyCode].Add(_command);
    }
}
