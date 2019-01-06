using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be used on an AI Object with the AICharacter script
// An AI Character can detect and move toward a target. Then it will perfom its personal action
public class AIDetection : TriggerZone {

    private AICharacter _aiCharacter;
    public List<string> vulnerableTo;
    private List<GameObject> _ennemies;

    private void Start()
    {
        _aiCharacter = this.transform.GetComponent<AICharacter>();
        _ennemies = new List<GameObject>();
        targetTag.AddRange(vulnerableTo);
    }

    // On Detection, send the target to the aiCharacter
    public override void TriggerEnter(Collider other)
    {
        // Danger, try to escape
        if (vulnerableTo.Contains(other.tag))
        {
            _targets.Remove(other.gameObject);
            _ennemies.Add(other.gameObject);
            _aiCharacter.EscapeFrom(other.gameObject);
        }
        // Target, try to attack
        else
        {
            _aiCharacter.TargetFound(other.gameObject);
        }
    }

    // On Detection Exit, 
    public override void TriggerExit(Collider other)
    {
        // If it's an enemy
        if (vulnerableTo.Contains(other.tag))
        {
            // Remove it from the list
            _ennemies.Remove(other.gameObject);
            // If there's no more enemies in sight
            if (_ennemies.Count == 0)
            {
                _aiCharacter.StopEscaping();
            }
            else
            {
                _aiCharacter.EscapeFrom(_ennemies[0]);
            }
        }
        // Remove the target from the list if nobody is near the target
        else
        {
            _aiCharacter.CheckIfRemoveTarget(other.gameObject);
        }        
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

    public GameObject GetEnemyNear()
    {
        if (_ennemies.Count == 0)
        {
            return null;
        }
        else
        {
            return _ennemies[0];
        }
    }
}
