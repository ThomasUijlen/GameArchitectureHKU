using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramStructure : MonoBehaviour
{
    private GameObject hologramObject;
    private GameObject resultStructure;

    private virtual void PositionStructure() {

    }

    private virtual bool CanPlace() {
        return true;
    }

    public bool TryPlaceStructure() {
        if(CanPlace()) {
            //Place structure
            return true;
        }

        return false;
    }
}
