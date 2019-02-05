using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWall : Building
{

    void Start()
    {
        _cost = new ResourcesPack { stone = stoneCost, wood = woodCost, food = foodCost, workForce = workForceCost, motivation = motivationCost };
    }

    public override void Crush()
    {
        //if (this.gameObject.CompareTag("StoneWall"))

    }


    public override bool ApplyEffect()
    {
        return true;
    }
}
