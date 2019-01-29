using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherDetection : TriggerZone {

    public int _radius= 5;


    // Use this for initialization
    void Start()
    {
        distanceDetection = _radius;
        GetComponent<SphereCollider>().radius = _radius;
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

    // On Detection Exit, 
    public override void TriggerExit(GameObject target)
    {
        
    }

    // On Collision, 
    public override void CollisionEnter(Collision collision)
    {
        
    }

    // On Collision Exit, 
    public override void CollisionExit(Collision collision)
    {
        
    }

    private void CallOut(GameObject target)
    {
        target.GetComponent<Villager>().JoinIn(gameObject);

    }
    

}
