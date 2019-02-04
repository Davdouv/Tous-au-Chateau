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
    public override void TriggerEnter(GameObject target)
    {
        //Debug.Log(this.name + " : TRIGGER ENTER : " + target.name);
        // Danger, try to escape
        if (vulnerableTo.Contains(target.tag))
        {
            //RemoveTarget(target);
            _ennemies.Add(target);
            _aiCharacter.EscapeFrom(target);
        }
        // Target, try to attack
        else
        {
            if (!_aiCharacter.IsTargetRegistered(target))
            {
                _aiCharacter.TargetFound(target);
            }
        }
    }

    // On Detection Exit, 
    public override void TriggerExit(GameObject target)
    {
        //Debug.Log(this.name + " : TRIGGER EXIT : " + target.name);
        // If it's an enemy
        if (vulnerableTo.Contains(target.tag))
        {
            // Remove it from the list
            _ennemies.Remove(target.gameObject);
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
            _aiCharacter.CheckIfRemoveTarget(target.gameObject);
        }        
    }

    // On Collision, stop moving
    public override void CollisionEnter(Collision collision)
    {
        // Check if it's the target we are aiming
        if (_aiCharacter.IsTheTarget(collision.gameObject))
        {
            //Debug.Log(this.name + " : COLLISION ENTER : " + collision.gameObject.name);
            _aiCharacter.ChangeOtherTarget(collision.gameObject); // Set a new target to the other members of the group
            _aiCharacter.DoActionOnTarget();
            _aiCharacter.StopMoving();
        }
    }

    // On Collision Exit, chase the target again
    public override void CollisionExit(Collision collision)
    {
        //Debug.Log(gameObject.name + "Collision Exit");
        if (_aiCharacter.IsTheTarget(collision.gameObject))
        {
            /*
            Debug.Log("Move Again");
            _aiCharacter.StopActionOnTarget();
            _aiCharacter.MoveAgain();
            */
        }
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

    public void RemoveEnnemy(GameObject ennemy)
    {
        _ennemies.Remove(ennemy);
    }
}
