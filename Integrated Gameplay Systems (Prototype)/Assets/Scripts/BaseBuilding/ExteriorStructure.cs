using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorStructure : HologramStructure
{
    private bool touchingTerrain = false;
    public ExteriorStructure(string _structureName, GameManager _gameManager) : base(_structureName, _gameManager) {
    }

    public override void PositionStructure()
    {
        base.PositionStructure();
        touchingTerrain = false;

        int layerMask = LayerMask.GetMask("Terrain");

        RaycastHit hit;
        if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hit, PLACE_DISTANCE, layerMask)) {
            touchingTerrain = true;
            hologramObject.transform.position = hit.point;
        }
    }

    public override bool CanPlace()
    {
        return touchingTerrain;
    }
}
