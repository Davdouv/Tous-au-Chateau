using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSign : Building
{

    void Start()
    {
        _cost.wood = 20;

    }

    public override bool ApplyEffect()
    {
        return true;
    }
}
