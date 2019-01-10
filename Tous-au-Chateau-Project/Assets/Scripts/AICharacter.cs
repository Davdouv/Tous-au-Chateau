﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
public class AICharacter : EnvironmentMaterial {
    
    private bool _isAlive;
    private NavMeshAgent _agent;
    private AICharactersGroup _assignedGroup;
    private GameObject _ownTarget;
    private Vector3 _destination;

    private float passiveSpeed = 2.0f;
    private float actionSpeed = 3.5f;

    private bool _hasPriorityOnTarget = false;
    private bool _isEscaping = false;
    private bool _isMovingAround = false;

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
    }

    // If target found, ask the group to share the target with all group objects
    public void TargetFound(GameObject target)
    {
        _hasPriorityOnTarget = true;
        _assignedGroup.AddTarget(target);
        _assignedGroup.ShareTarget(target);
    }

    public void TargetNotFound()
    {
        _hasPriorityOnTarget = false;
        _assignedGroup.ShareNoTarget();
    }

    public void ChangeOtherTarget(GameObject target)
    {
        _assignedGroup.ChangeTarget(target, this);
    }

    public void CheckIfRemoveTarget(GameObject target)
    {
        _hasPriorityOnTarget = false;
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

    private void SetDestination(Vector3 destination)
    {
        if (gameObject.activeSelf)
        {
            _destination = destination;
            _agent.SetDestination(destination);
        }
    }

    // Used by the AICharactersGroup
    // Set Destination only if there was no destination before
    public void SetTarget(GameObject target)
    {
        SetIsMovingAround(false);
        if ((_ownTarget == null && !_isEscaping) || (_ownTarget != null && !_hasPriorityOnTarget)) // No Target before OR didn't have priority on target
        {
            _ownTarget = target;
            if (_ownTarget) // Make sure target is not null
            {
                Stop(false);
                FastSpeed();
                SetDestination(target.transform.position);
            }
        }
    }

    // Set no target
    public void NoTarget()
    {
        _ownTarget = null;
        _destination = _assignedGroup.GetRallyPoint().transform.position;
        SlowSpeed();
        Stop(true);
    }

    // Used by the AICharactersGroup
    public void Stop(bool stop)
    {
        if (gameObject.activeSelf)
        {
            //_agent.isStopped = stop;
            _agent.isStopped = false;
        }        
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
        if (objectToDestroy && objectToDestroy.name != "RallyPoint")
        {
            objectToDestroy.SetActive(false);
        }        
    }

    // Remove the item from the list and get a new target
    protected void GetNewTarget()
    {
        _assignedGroup.RemoveItem(_ownTarget);
        _assignedGroup.CancelTarget(_ownTarget);
        _assignedGroup.NewTarget();
    }

    // Just for security
    public bool IsTheTarget(GameObject target)
    {
        return (target == _ownTarget);
    }

    // Check if RallyPoint has reached destination
    private bool IsDestinationReached(float stoppingDistance)
    {
        return (Vector3.Distance(_destination, transform.position) < stoppingDistance);
    }

    // We need to update the navMesh Destination if the target is moving
    private void Update()
    {
        if (_ownTarget)
        {
            // Update the destination in case the target is moving
            _agent.SetDestination(_ownTarget.transform.position);

            // If we are regrouping
            if (_assignedGroup.IsRegrouping())
            {
                // Stop when one member has join the rally point (the actual target)
                if (IsDestinationReached(1.25f))
                {
                    _assignedGroup.StopRegrouping();
                    _assignedGroup.ShareNoTarget();
                    _assignedGroup.MoveRandom();
                }
            }
        }
        if (_isEscaping)
        {
            GameObject ennemy = GetComponent<AIDetection>().GetEnemyNear();
            // If there's still an ennemy close to us
            if (ennemy && Vector3.Distance(ennemy.transform.position, transform.position) < GetComponent<AIDetection>().distanceDetection)
            {
                EscapeFrom(ennemy);
            }
            else
            {
                StopEscaping();
            }
        }
        if (_isMovingAround)
        {
            if (IsDestinationReached(1.25f))
            {
                SetRandomDestination(_assignedGroup.RandomNavmeshLocation());
            }
        }
    }

    // Set a Destination (not a target gameobject)
    public void SetRandomDestination(Vector3 destination)
    {
        Stop(false);
        SlowSpeed();
        SetDestination(destination);
    }

    // Used by the AICharactersGroup
    public void SetSlowSpeed(float speed)
    {
        passiveSpeed = speed;
    }
    public void SetFastSpeed(float speed)
    {
        actionSpeed = speed;
    }

    // Used to change the speed of a character
    private void SlowSpeed()
    {
        if (gameObject.activeSelf)
        {
            _agent.speed = passiveSpeed;
        }        
    }
    private void FastSpeed()
    {
        if (gameObject.activeSelf)
        {
            _agent.speed = actionSpeed;
        }
    }

    // Go away from the enemy
    public void EscapeFrom(GameObject enemy)
    {
        _isEscaping = true;
        _ownTarget = null;
        Stop(false);
        FastSpeed();
        // Calculate the newPosition where we must go
        Vector3 distance = transform.position - enemy.transform.position;
        Vector3 newPos = transform.position + distance;
        SetDestination(newPos);
    }

    // Try to find a new target or join the rally point
    public void StopEscaping()
    {
        _isEscaping = false;
        _assignedGroup.NewTarget();
    }

    public void SetIsMovingAround(bool isMovingAround)
    {
        _isMovingAround = isMovingAround;
    }
}
