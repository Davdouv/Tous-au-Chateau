using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWall : Building
{
    public override void SetHasLanded()
    {
        base.SetHasLanded();

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
}
