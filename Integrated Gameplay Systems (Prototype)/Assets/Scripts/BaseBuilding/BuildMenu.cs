using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BuildMenu : Menu
{
    private static Dictionary<string, StructureInformation> structureLibrary = new Dictionary<string, StructureInformation> {
        {"Hub", new StructureInformation(typeof(ExteriorStructure), "A large structure. Has a decent amount of interior space")}
    };

    private OpenMenuCommand backCommand;
    private GameObject buildMenu;
    private string selectedStructure = null;

    public BuildMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = false;
        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Escape, backCommand);
        CreateStructureList();

        GameObject.Find("BuildButton").GetComponent<Button>().onClick.AddListener(() => StructureConfirm());
        GameObject.Find("DemolishButton").GetComponent<Button>().onClick.AddListener(() => DemolishConfirm());
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Escape, backCommand);
        GameObject.Destroy(buildMenu);
    }

    private void CreateStructureList() {
        buildMenu = gameManager.prefabLibrary.InstantiatePrefab("BuildMenu");

        UIList structureList = new UIList(gameManager.prefabLibrary.GetPrefab("StructureButton"), GameObject.Find("StructureList"), 85f);
        foreach(KeyValuePair<string, StructureInformation> keyValuePair in structureLibrary) {
            string structureName = keyValuePair.Key;
            StructureInformation structureInformation = keyValuePair.Value;
                        
            GameObject structureButton = structureList.AddElement();
            structureButton.GetComponentInChildren<Text>().text = structureName;
            structureButton.GetComponent<Button>().onClick.AddListener(() => StructureSelected(structureName));
            if(selectedStructure == null) StructureSelected(structureName);
        }
    }

    public void StructureSelected(string structure) {
        selectedStructure = structure;
        GameObject.Find("StructureName").GetComponent<Text>().text = structure;
        GameObject.Find("StructureDescription").GetComponent<Text>().text = structureLibrary[structure].description;
    }

    public void StructureConfirm() {
        PlaceMenu placeMenu = new PlaceMenu(stateMachine, gameManager);
        placeMenu.SetStructure(selectedStructure);
        stateMachine.SetState(placeMenu);
    }

    public void DemolishConfirm() {
        stateMachine.SetState(new DemolishMenu(stateMachine, gameManager));
    }

    private struct StructureInformation {
        public Type hologramType;
        public string description;

        public StructureInformation(Type _hologramType, string _description) {
            hologramType = _hologramType;
            description = _description;
        }
    }
}
