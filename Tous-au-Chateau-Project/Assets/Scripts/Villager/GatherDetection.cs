using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherDetection : TriggerZone {
    
    // On Detection, 
    public override void TriggerEnter(GameObject target)
    {
        if (target.GetComponent<Villager>().GetStats().IsAlive())
        {
            if (target.GetComponent<Villager>().IsPassive() && !GetComponent<Villager>().IsPassive())
            {
                //Debug.Log(name + " GATHERING TRIGGER ENTER : " + target.name);
                CallOut(target);
            }
        }        
    }

    private void CallOut(GameObject target)
    {
        target.GetComponent<Villager>().JoinIn(gameObject);
    }
}
