using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be used on an AI Object with the AICharacter script
// An AI Character can detect and move toward a target. Then it will perfom its personal action
public class AIDetection : TriggerZone {

    private AICharacter _aiCharacter;
    private List<GameObject> _targetDetected;

    private void Start()
    {
        _aiCharacter = this.transform.GetComponent<AICharacter>();
        _targetDetected = new List<GameObject>();
    }

    // On Detection, send the target to the aiCharacter
    public override void TriggerEnter(Collider other)
    {
        _targetDetected.Add(other.gameObject);
        _aiCharacter.TargetFound(other.gameObject);
    }

    // On Detection Exit, remove the target from the list
    public override void TriggerExit(Collider other)
    {
        _targetDetected.Remove(other.gameObject);
        // If it was the target we were aiming
        if (_aiCharacter.IsTheTarget(other.gameObject))
        {
            // Cancel this target
            _aiCharacter.NoTarget();
            // Check if there's a new one near
            NewTarget(other.gameObject);
        }
    }

    // On Collision, stop moving
    public override void CollisionEnter(Collision collision)
    {
        // Check if it's the target we are aiming
        if (_aiCharacter.IsTheTarget(collision.gameObject))
        {
            _aiCharacter.Stop(true);
            _aiCharacter.DoActionOnTarget();
        }
    }

    // On Collision Exit, chase the target again
    public override void CollisionExit(Collision collision)
    {
        _aiCharacter.Stop(false);
    }

    // Send a new target if there's one on the list
    public void NewTarget(GameObject oldTarget)
    {
        //_aiCharacter.Stop(false);
        if (_targetDetected.Count == 0)
        {
            _aiCharacter.TargetNotFound();
        }
        else
        {
            _aiCharacter.TargetFound(_targetDetected[0]);
        }
    }
    
    public void RemoveItem(GameObject item)
    {
        _targetDetected.Remove(item);
    }
}
