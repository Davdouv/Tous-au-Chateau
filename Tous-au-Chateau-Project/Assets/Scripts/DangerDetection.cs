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
        if (collision.gameObject.GetComponent<Building>())
        {
            _onPlatform = true;
        }

        if (collision.gameObject.tag == "River")
        {
            if (!_onPlatform)
                _stats.TakeDamage(9999,DeathReason.RIVER);
                return;
        }
        if (collision.gameObject.tag == "Void")
        {
            if (!_onPlatform)
                _stats.TakeDamage(9999, DeathReason.VOID);
            return;
        }
    }

    public override void CollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Building>())
        {
            _onPlatform = false;
        }
    }
    
    
    
}
