using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherDetection : TriggerZone {

    public int _radius= 5;


    // Use this for initialization
    void Start()
    {
        distanceDetection = _radius;
        SphereCollider reach = gameObject.AddComponent<SphereCollider>();
        reach.radius = _radius;
        reach.isTrigger = true;
        targetTag.Add("Villager");
    }


    // On Detection, 
    public override void TriggerEnter(GameObject target)
    {
        
        // 
        if (target.tag == "Villager")
        {
            if (target.GetComponent<Villager>().IsPassive() && !GetComponent<Villager>().IsPassive())
            {
                Debug.Log(name + " GATHERING TRIGGER ENTER : " + target.name);
                CallOut(target);
            }
        }

    }
    

    private void CallOut(GameObject target)
    {
        target.GetComponent<Villager>().JoinIn(gameObject);

    }
    

}
