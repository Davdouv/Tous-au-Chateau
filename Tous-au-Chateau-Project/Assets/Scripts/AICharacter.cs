using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
public class AICharacter : EnvironmentMaterial {
    
    private bool _isAlive;
    private NavMeshAgent _agent;
    private NavMeshObstacle _obstacle;
    private AICharactersGroup _assignedGroup;
    private GameObject _ownTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _obstacle = GetComponent<NavMeshObstacle>();
    }

    private void Start()
    {
        SetGroup();
    }

    // Get the group and add him to it
    private void SetGroup()
    {
        _assignedGroup = transform.parent.GetComponent<AICharactersGroup>();
        _assignedGroup.Add(GetComponent<AICharacter>());
    }

    // If target found, ask the group to share the target with all group objects
    public void TargetFound(GameObject target)
    {
        _assignedGroup.ShareTarget(target);
    }

    public void TargetNotFound()
    {
        _assignedGroup.ShareNoTarget();
    }

    // Used by the AICharactersGroup
    // Set Destination only if there was no destination before
    public void SetTarget(GameObject target)
    {
        if (_ownTarget == null) // No Destination before
        {
            _ownTarget = target;
            if (_ownTarget) // Make sur target is not null
            {
                _agent.isStopped = false;
                _agent.SetDestination(target.transform.position);
            }
        }
    }

    public void NoTarget()
    {
        _ownTarget = null;
        _agent.isStopped = true;
    }

    // Used by the AICharactersGroup
    public void Stop(bool stop)
    {
        if (stop)
        {
            _agent.enabled = false;
            _obstacle.enabled = true;
        }
        else
        {
            _obstacle.enabled = false;
            _agent.enabled = true;
        }
        // We do this to avoid having the warning of both components active at the same time
        //_agent.enabled = !stop;
        //_obstacle.enabled = stop;
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
        yield return new WaitForSeconds(2);
        GameObject objectToDestroy = _ownTarget;
        GetNewTarget();
        Destroy(objectToDestroy);
    }

    protected void GetNewTarget()
    {
        AIDetection aiDetection = GetComponent<AIDetection>();
        aiDetection.RemoveItem(_ownTarget);
        _ownTarget = null;
        GetComponent<AIDetection>().NewTarget(_ownTarget);
    }

    public bool IsTheTarget(GameObject target)
    {
        return (target == _ownTarget);
    }
}
