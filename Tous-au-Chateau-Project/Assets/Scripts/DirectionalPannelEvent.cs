using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalSign))]
public class DirectionalPannelEvent : TriggerZone {

    private bool _isOnTheGround;
    private DirectionalSign _directionalSign;

    private void Start()
    {
        _directionalSign = GetComponent<DirectionalSign>();
    }

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
            target.GetComponent<Villager>().ChangeDirection(_directionalSign.direction);
        }        
    }
}
