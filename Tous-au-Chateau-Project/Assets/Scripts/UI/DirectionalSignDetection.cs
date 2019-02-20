using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalSign))]
public class DirectionalSignDetection : BuildingDetection
{
    private DirectionalSign _directionalSign;
    public string villagerTag;

    protected override void Init()
    {
        base.Init();
        _directionalSign = GetComponent<DirectionalSign>();
        targetTag.Add(villagerTag);
    }

    public override void TriggerEnter(GameObject target)
    {
        if (_isPlaced && target.CompareTag(villagerTag))
        {
            target.GetComponent<Villager>().ChangeDirection(_directionalSign.direction);
        }
    }
}
