using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BasicObject
{
    public enum INPUT_MODE {
        SINGLE_PRESSED,
        PRESSED,
        SINGLE_RELEASED,
        RELEASED
    }
    private Dictionary<KeyCode, Dictionary<ICommand, INPUT_MODE>> keyBindings = new Dictionary<KeyCode, Dictionary<ICommand, INPUT_MODE>>();
    private Dictionary<KeyCode, Dictionary<ICommand, INPUT_MODE>> newBindings = new Dictionary<KeyCode, Dictionary<ICommand, INPUT_MODE>>();
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
            foreach(KeyValuePair<ICommand, INPUT_MODE> keyValuePair in keyBindings[key]) {
                switch(keyValuePair.Value) {
                    case INPUT_MODE.SINGLE_PRESSED:
                    if(Input.GetKeyDown(key)) keyValuePair.Key.Execute();
                    break;
                    case INPUT_MODE.PRESSED:
                    if(Input.GetKey(key)) keyValuePair.Key.Execute();
                    break;
                    case INPUT_MODE.SINGLE_RELEASED:
                    if(Input.GetKeyUp(key)) keyValuePair.Key.Execute();
                    break;
                    case INPUT_MODE.RELEASED:
                    if(!Input.GetKey(key)) keyValuePair.Key.Execute();
                    break;
                }
                
            }
        }
    }

    private void IntegrateNewKeys() {
        foreach(KeyCode key in newBindings.Keys) {
            if(!keyBindings.ContainsKey(key)) keyBindings.Add(key, new Dictionary<ICommand, INPUT_MODE>());
            foreach(KeyValuePair<ICommand, INPUT_MODE> keyValuePair in newBindings[key]) keyBindings[key].Add(keyValuePair.Key, keyValuePair.Value);
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

    public void RegisterKeyBinding(KeyCode _keyCode, ICommand _command, INPUT_MODE _inputMode = INPUT_MODE.SINGLE_PRESSED) {
        if(!newBindings.ContainsKey(_keyCode)) newBindings.Add(_keyCode, new Dictionary<ICommand, INPUT_MODE>());
        newBindings[_keyCode].Add(_command, _inputMode);
    }

    public void DeregisterKeyBinding(KeyCode _keyCode, ICommand _command) {
        if(!oldBindings.ContainsKey(_keyCode)) oldBindings.Add(_keyCode, new List<ICommand>());
        oldBindings[_keyCode].Add(_command);
    }
}
