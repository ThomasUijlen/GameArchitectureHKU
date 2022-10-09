using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BuildMenu : Menu
{
    private static Dictionary<string, StructureInformation> structureLibrary = new Dictionary<string, StructureInformation> {
        {"Hub", new StructureInformation(typeof(ExteriorStructure), "A large structure. Has a decent amount of interior space")},
        {"Storage Container", new StructureInformation(typeof(InteriorStructure), "A storage container. Can only be placed inside.")},
        {"ClassDefaultCrafter", new StructureInformation(typeof(InteriorStructure), "A crafter. Can be used to convert items into complexer ones. Should be placed inside.")},
        {"ClassItemEnhancer", new StructureInformation(typeof(InteriorStructure), "An item enhancer. Enhanced the gold value of items. Should be placed inside.")}
    };

    private OpenMenuCommand backCommand;
    private GameObject buildMenu;
    private string selectedStructure = null;

    public BuildMenu(IStateMachine _stateMachine, GameManager _gameManager) : base(_stateMachine, _gameManager) {
        allowMovement = false;
        backCommand = new OpenMenuCommand(typeof(NoMenu), _stateMachine, _gameManager);
    }

    public override void EnableState() {
        gameManager.inputManager.RegisterKeyBinding(KeyCode.Tab, backCommand);
        CreateStructureList();

        GameObject.Find("BuildButton").GetComponent<Button>().onClick.AddListener(() => StructureConfirm());
        GameObject.Find("DemolishButton").GetComponent<Button>().onClick.AddListener(() => DemolishConfirm());
    }

    public override void DisableState() {
        gameManager.inputManager.DeregisterKeyBinding(KeyCode.Tab, backCommand);
        GameObject.Destroy(buildMenu);
    }

    private void CreateStructureList() {
        buildMenu = gameManager.prefabLibrary.InstantiatePrefab("BuildMenu");

        UIList structureList = new UIList(gameManager.prefabLibrary.GetPrefab("StructureButton"), GameObject.Find("StructureList"), 85f);
        foreach(KeyValuePair<string, StructureInformation> keyValuePair in structureLibrary) {
            string structureName = keyValuePair.Key;
            StructureInformation structureInformation = keyValuePair.Value;
                        
            GameObject structureButton = structureList.AddElement();
            string structureNameText = (structureName.Length > 6 && structureName.Substring(0, 5) == "Class") ? structureName.Substring(5) : structureName;
            structureButton.GetComponent<Button>().onClick.AddListener(() => StructureSelected(structureName));
            structureButton.GetComponentInChildren<Text>().text = structureNameText;
            if(selectedStructure == null) StructureSelected(structureName);
        }
    }

    public void StructureSelected(string _structure) {
        selectedStructure = _structure;
        string structureNameText = (_structure.Length > 6 && _structure.Substring(0, 5) == "Class") ? _structure.Substring(5) : _structure;
        GameObject.Find("StructureName").GetComponent<Text>().text = structureNameText;
        GameObject.Find("StructureDescription").GetComponent<Text>().text = structureLibrary[_structure].description;
    }

    public void StructureConfirm() {
        PlaceMenu placeMenu = new PlaceMenu(stateMachine, gameManager);
        placeMenu.SetStructure(selectedStructure, structureLibrary[selectedStructure].hologramType);
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
