using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Class to handle collisions and detections with box / sphere colliders
public abstract class TriggerZone : MonoBehaviour {

    public List<string> targetTag = new List<string>();
    public float distanceDetection = 5.0f;
    
    protected bool _isInContact;
    protected List<Target> _targetList = new List<Target>();

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
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
            _targetList.Add(new Target(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetTag.Contains(other.gameObject.tag))
        {
            RemoveTarget(other.gameObject);
        }
    }

    // ***** COLLISION ***** //
    // Trigger must be disabled on collider
    private void OnCollisionEnter(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = true;

            CollisionEnter(collision);
        }
        else
        {
            CollisionEnterOther(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (targetTag.Contains(collision.gameObject.tag))
        {
            _isInContact = false;

            CollisionExit(collision);
        }
        else
        {
            CollisionExitOther(collision);
        }
    }

    // Not used anymore
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
    }

    public bool IsInContact()
    {
        return _isInContact;
    }

    public void AddTarget(GameObject target)
    {
        _targetList.Add(new Target(target));
    }

    public virtual void TriggerEnter(GameObject target) { }
    public virtual void TriggerExit(GameObject target) { }
    public virtual void CollisionEnter(Collision collision) { }
    public virtual void CollisionExit(Collision collision) { }
    public virtual void CollisionEnterOther(Collision collision) { }
    public virtual void CollisionExitOther(Collision collision) { }

    public bool IsInRange(Vector3 position)
    {
        float distance = (transform.position - position).sqrMagnitude;
        return (distance < distanceDetection*distanceDetection);
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
        for (int i = 0; i < _targetList.Count; ++i)
        {
            if (_targetList[i] != null)
            {
                // If not triggered yet, check if it's a the good distance
                if (!_targetList[i].IsTriggered() && IsInRange(_targetList[i].GetObject().transform.position))
                {
                    _targetList[i].SetTrigger(true);
                    TriggerEnter(_targetList[i].GetObject());
                }
                // If it has been triggered, check if it get away
                else if (_targetList[i].IsTriggered() && !IsInRange(_targetList[i].GetObject().transform.position))
                {
                    _targetList[i].SetTrigger(false);
                    TriggerExit(_targetList[i].GetObject());
                }
                // If the object is no longer active
                else if (!_targetList[i].GetObject().activeSelf)
                {
                    TriggerExit(_targetList[i].GetObject());
                    RemoveTarget(_targetList[i].GetObject());
                    --i;
                }
            }
            
        }
    }
}
