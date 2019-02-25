using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DangerDetection : TriggerZone {
    
    private bool _onPlatform = false;
    private CharacterStats _stats;

    private void Start()
    {
        _stats = GetComponent<CharacterStats>();
    }

    public override void CollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER : " + collision.gameObject.name);
        if (collision.gameObject.tag == "Platform")
        {
            _onPlatform = true;
        }

        if (collision.gameObject.tag == "DangerArea")
        {
            if (!_onPlatform)
                _stats.TakeDamage(9999);
                return;
        }
    }

    public override void CollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _onPlatform = false;
        }
    }
    
    
    
}
