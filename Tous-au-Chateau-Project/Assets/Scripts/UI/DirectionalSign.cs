using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSign : Building
{
    enum Direction { North, East, South, West };

    void Start()
    {
        _cost.wood = 20;

    }

    public override bool ApplyEffect()
    {
        return true;
    }
}
