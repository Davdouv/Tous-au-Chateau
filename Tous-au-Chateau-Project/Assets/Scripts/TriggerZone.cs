using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Class to handle collisions and detections with box / sphere colliders
public abstract class TriggerZone : MonoBehaviour {

    public List<string> targetTag = new List<string>();
    public float distanceDetection = 5.0f;

    //protected List<GameObject> _targets = new List<GameObject>();
    protected bool _isInContact;

    protected List<Target> _targetList = new List<Target>();

    private void Start()
    {
        SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
        if (sphereCollider && sphereCollider.isTrigger)
        {
            sphereCollider.radius = distanceDetection;
        }
    }

    // ***** DETECTION *****/
    // Trigger must be enabled on collider
    private void OnTriggerEnter(Collider other)
    {
        // If the target's tag is in the list
        if (targetTag.Contains(other.gameObject.tag))
        {
            //_targets.Add(other.gameObject);
            _targetList.Add(new Target(other.gameObject));

            // Debug.Log("Target " + other.gameObject.name + " in sight");

            //TriggerEnter(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            //_targets.Remove(other.gameObject);
            RemoveTarget(other.gameObject);

            //Debug.Log("Target " + other.gameObject.name + " got away");

            //TriggerExit(other);
        }
    }

    // ***** COLLISION ***** //
    // Trigger must be disabled on collider
    private void OnCollisionEnter(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = true;

            //Debug.Log("Collision with " + collision.gameObject.name + " !");

            CollisionEnter(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = false;

            //Debug.Log("Collision with " + collision.gameObject.name + " no more");

            CollisionExit(collision);
        }
    }

    public bool IsInSight(GameObject target)
    {
        foreach (Target tar in _targetList)
        {
            if (tar.GetObject() == target)
            {
                return true;
            }
        }
        return false;
        
        //return _targets.Contains(target);
    }

    public bool IsInContact()
    {
        return _isInContact;
    }

    public void AddTarget(GameObject target)
    {
        //_targets.Add(target);
        _targetList.Add(new Target(target));
    }

    public virtual void TriggerEnter(GameObject target) { }
    public virtual void TriggerExit(GameObject target) { }
    public virtual void CollisionEnter(Collision collision) { }
    public virtual void CollisionExit(Collision collision) { }

    private bool IsInRange(Vector3 position)
    {
        float distance = Vector3.Distance(transform.position, position);
        return (distance < distanceDetection);
    }

    protected void RemoveTarget(GameObject target)
    {
        foreach(Target tar in _targetList)
        {
            if (tar.GetObject() == target)
            {
                _targetList.Remove(tar);
                return;
            }
        }
    }

    private void Update()
    {        
        foreach (Target target in _targetList)
        {
            // If not triggered yet, check if it's a the good distance
            if (!target.IsTriggered() && IsInRange(target.GetObject().transform.position))
            {
                target.Trigger();
                TriggerEnter(target.GetObject());
            }
        }
    }
}
