using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalSign : Building
{

    void Start()
    {
        _cost.wood = 20;

    }

    public override void crush()
    {
        //if (this.gameObject.CompareTag("DirectionalSign"))
      
    }


    public override bool ApplyEffect()
    {
        return true;
    }
}
