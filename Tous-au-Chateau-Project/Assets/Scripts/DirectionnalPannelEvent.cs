using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionnalPannelEvent : TriggerZone {

    private bool _isOnTheGround;

    public override void CollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isOnTheGround = true;
        }        
    }

    public override void CollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isOnTheGround = false;
        }
    }

    public override void TriggerEnter(GameObject target)
    {
        if (_isOnTheGround && target.tag != "Ground")
        {
            // TODO 
            // --> Need to change BoidMovement to Villager
            // --> Need to change "LeftWoodSign" to DirectionnalPannel.GetDirection
            // --> Need to change Turn method in Villager
            target.GetComponent<BoidMovement>().Turn("LeftWoodSign");
        }        
    }
}
