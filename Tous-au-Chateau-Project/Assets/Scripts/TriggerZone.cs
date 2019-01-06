using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Class to handle collisions and detections with box / sphere colliders
public abstract class TriggerZone : MonoBehaviour {

    public List<string> targetTag = new List<string>();

	protected List<GameObject> _targets = new List<GameObject>();
    protected bool _isInContact;

    // ***** DETECTION *****/
    // Trigger must be enabled on collider
    private void OnTriggerEnter(Collider other)
    {
        // If the target's tag is in the list
        if (targetTag.Contains(other.gameObject.tag))
        {
            _targets.Add(other.gameObject);

            Debug.Log("Target in sight");

            TriggerEnter(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            _targets.Remove(other.gameObject);

            Debug.Log("Target got away");

            TriggerExit(other);
        }
    }

    // ***** COLLISION ***** //
    // Trigger must be disabled on collider
    private void OnCollisionEnter(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = true;

            Debug.Log("Collision !");

            CollisionEnter(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = false;

            Debug.Log("Collision no more");

            CollisionExit(collision);
        }
    }

    public bool IsInSight(GameObject target)
    {
        return _targets.Contains(target);
    }

    public bool IsInContact()
    {
        return _isInContact;
    }

    public List<GameObject> GetTargets()
    {
        return _targets;
    }

    public void AddTarget(GameObject target)
    {
        _targets.Add(target);
    }

    public virtual void TriggerEnter(Collider other) { }
    public virtual void TriggerExit(Collider other) { }
    public virtual void CollisionEnter(Collision collision) { }
    public virtual void CollisionExit(Collision collision) { }
}
