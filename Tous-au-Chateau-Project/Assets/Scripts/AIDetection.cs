using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be used on an AI Object with the AICharacter script
// An AI Character can detect and move toward a target. Then it will perfom its personal action
public class AIDetection : TriggerZone {

    private AICharacter _aiCharacter;

    private void Start()
    {
        _aiCharacter = this.transform.GetComponent<AICharacter>();
    }

    // On Detection, send the target to the aiCharacter
    public override void TriggerEnter(Collider other)
    {
        _aiCharacter.TargetFound(other.gameObject);
    }

    // On Detection Exit, remove the target from the list if nobody is near the target
    public override void TriggerExit(Collider other)
    {
        _aiCharacter.CheckIfRemoveTarget(other.gameObject);
    }

    // On Collision, stop moving
    public override void CollisionEnter(Collision collision)
    {
        // Check if it's the target we are aiming
        if (_aiCharacter.IsTheTarget(collision.gameObject))
        {
            _aiCharacter.Stop(true);
            _aiCharacter.ChangeOtherTarget(collision.gameObject);
            _aiCharacter.DoActionOnTarget();
        }
    }

    // On Collision Exit, chase the target again
    public override void CollisionExit(Collision collision)
    {
        _aiCharacter.Stop(false);
    }
}
