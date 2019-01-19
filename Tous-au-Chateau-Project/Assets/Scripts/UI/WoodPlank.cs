using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Building {

    public void crush()
    {
        if(this.gameObject.CompareTag("bridge") == "")
        {

        }
    }
    public bool canBuy()
    {
        if (this.gameObject.CompareTag("bridge") == "")
        {

        }
    }

    public bool ApplyEffect()
    {
        return;
    }

}
