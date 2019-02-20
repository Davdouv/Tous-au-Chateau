using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class BuildingDetection : TriggerZone
{
    protected bool _isPlaced;

    public override void CollisionEnterOther(Collision collision)
    {
        // When the building hit something other than the controllers, then it's placed
        _isPlaced = true;
    }

    public override void CollisionExitOther(Collision collision)
    {
        _isPlaced = false;
    }
}
