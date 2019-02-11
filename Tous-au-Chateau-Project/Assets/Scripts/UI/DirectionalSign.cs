using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { RIGHT, LEFT, BACKWARD };

public class DirectionalSign : Building
{
    public Direction direction;

    void Start()
    {
        _cost = new ResourcesPack { stone = stoneCost, wood = woodCost, food = foodCost, workForce = workForceCost, motivation = motivationCost };
    }

    public override bool ApplyEffect()
    {
        return true;
    }
}
