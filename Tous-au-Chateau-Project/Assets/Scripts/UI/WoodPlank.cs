using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPlank : Building
{

    void Start()
    {
        _cost.wood = 5;

    }

    public override void crush()
    {
       // if(this.gameObject.CompareTag("woodPlank"))
        
    }
    

    public override bool ApplyEffect()
    {
        return true;
    }

}
