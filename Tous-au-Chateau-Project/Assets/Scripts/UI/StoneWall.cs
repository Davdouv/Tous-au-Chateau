using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWall : Building
{

    public override void Crush()
    {
        //if (this.gameObject.CompareTag("StoneWall"))
    }


    public override bool ApplyEffect()
    {
        return true;
    }
}
