using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorStructure : HologramStructure
{
    private bool touchingTerrain = false;
    private bool isInside = false;
    public InteriorStructure(string _structureName, GameManager _gameManager) : base(_structureName, _gameManager) {
    }

    public override void PositionStructure()
    {
        base.PositionStructure();
        touchingTerrain = false;
        isInside = false;

        int layerMask = LayerMask.GetMask("Structure");

        RaycastHit hit;
        if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out hit, PLACE_DISTANCE, layerMask)) {
            touchingTerrain = true;
            hologramObject.transform.position = hit.point;

            int interiorLayer = LayerMask.GetMask("Interior");
            foreach(Collider collider in Physics.OverlapSphere(hologramObject.transform.position, 10f, interiorLayer)) {
                if(collider.bounds.Contains(hologramObject.transform.position)) {
                    isInside = true;
                    break;
                }
            }
        }
    }

    public override bool CanPlace()
    {
        return touchingTerrain && isInside;
    }
}
