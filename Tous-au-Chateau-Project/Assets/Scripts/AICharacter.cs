using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
public class AICharacter : EnvironmentMaterial {
    
    private bool _isAlive;
    private NavMeshAgent _agent;
    private AICharactersGroup _assignedGroup;
    private GameObject _ownTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetGroup();
    }

    // Get the group and add him to it
    private void SetGroup()
    {
        _assignedGroup = transform.parent.GetComponent<AICharactersGroup>();
        _assignedGroup.AddCharacter(this);
    }

    // If target found, ask the group to share the target with all group objects
    public void TargetFound(GameObject target)
    {
        _assignedGroup.AddTarget(target);
        _assignedGroup.ShareTarget(target);
    }

    public void TargetNotFound()
    {
        _assignedGroup.ShareNoTarget();
    }

    public void ChangeOtherTarget(GameObject target)
    {
        _assignedGroup.ChangeTarget(target, this);
    }

    public void CheckIfRemoveTarget(GameObject target)
    {
        _assignedGroup.CheckIfRemoveTarget(target);

        // If it was the target we were aiming
        if (IsTheTarget(target))
        {
            // Cancel this target
            NoTarget();
            // Check if there's a new one near
            _assignedGroup.NewTarget();
        }
    }

    // Used by the AICharactersGroup
    // Set Destination only if there was no destination before
    public void SetTarget(GameObject target)
    {
        if (_ownTarget == null) // No Destination before
        {
            _ownTarget = target;
            if (_ownTarget) // Make sure target is not null
            {
                Stop(false);
                _agent.SetDestination(target.transform.position);
                Debug.Log("SetTarget : " + target.name);
            }
        }
    }

    // Set no target
    public void NoTarget()
    {
        _ownTarget = null;
        Stop(true);
    }

    // Used by the AICharactersGroup
    public void Stop(bool stop)
    {
        _agent.isStopped = stop;
    }

    // What to do when Obstacle Reached ?
    // This method MUST call GetNewTarget() when it's done and before destroying the target
    public virtual void DoActionOnTarget()
    {
        // FOR TEST
        StartCoroutine(DestroyTarget());
    }

    // Personal action (for test)
    private IEnumerator DestroyTarget()
    {
        yield return new WaitForSeconds(1); // This is bad
        GameObject objectToDestroy = _ownTarget;
        GetNewTarget();
        //Destroy(objectToDestroy);
        objectToDestroy.SetActive(false);
    }

    // Remove the item from the list and get a new target
    protected void GetNewTarget()
    {
        //AIDetection aiDetection = GetComponent<AIDetection>();
        _assignedGroup.RemoveItem(_ownTarget);
        //_ownTarget = null;
        _assignedGroup.CancelTarget(_ownTarget);
        //NoTarget();
        _assignedGroup.NewTarget();
    }

    // Just for security
    public bool IsTheTarget(GameObject target)
    {
        return (target == _ownTarget);
    }

    // We need to update the navMesh Destination if the target is moving
    private void Update()
    {
        if (_ownTarget)
        {
            _agent.SetDestination(_ownTarget.transform.position);
        }        
    }
}
