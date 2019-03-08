using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalSign))]
public class DirectionalSignDetection : TriggerZone
{
    private DirectionalSign _directionalSign;

    protected override void Init()
    {
        base.Init();
        _directionalSign = GetComponent<DirectionalSign>();
    }

    public override void TriggerEnter(GameObject target)
    {
        if (_directionalSign.HasLanded())
        {
            Debug.Log("LANDED");
            target.GetComponent<Villager>().ChangeDirection(_directionalSign.direction);
        }
    }
}
