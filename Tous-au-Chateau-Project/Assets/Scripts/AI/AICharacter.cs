using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
[RequireComponent(typeof(AICharacterAttack))]
public class AICharacter : EnvironmentMaterial {
    
    // Components attached to object
    private NavMeshAgent _agent;
    private AICharacterAttack _combat;

    private AICharactersGroup _assignedGroup;

    // Target & destination for navMesh agent
    private GameObject _ownTarget;
    private Vector3 _destination;

    // These variables are set by the assignedGroup !
    private float passiveSpeed = 2.0f;
    private float actionSpeed = 3.5f;

    private float _stoppingDistance = 1.5f;

    // States
    private bool _hasPriorityOnTarget = false;
    private bool _isEscaping = false;
    private bool _isMovingAround = false;
    private bool _isAttacking = false;
    private bool _isAttacked = false;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _combat = GetComponent<AICharacterAttack>();
        _assignedGroup = transform.parent.GetComponent<AICharactersGroup>();
    }

    // Get the group and add him to it
    public bool IsAttacking()
    {
        return _isAttacking;
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
                FastSpeed();
                SetDestination(target.transform.position);
            }
        }
    }

    // Set no target
    public void NoTarget()
    {
        _hasPriorityOnTarget = false;
        _ownTarget = null;
        _destination = _assignedGroup.GetRallyPoint().transform.position;
        SlowSpeed();
    }

    // What to do when Obstacle Reached ?
    // This method MUST call GetNewTarget() when it's done and before destroying the target
    public virtual void DoActionOnTarget()
    {
        _hasPriorityOnTarget = true;
        _isAttacking = true;        
    }

    // No need for this
    private void StopTarget()
    {
        // Make the target stop moving because we are attacking it
        AICharacter aiTarget = _ownTarget.GetComponent<AICharacter>();
        if (aiTarget)
        {
            aiTarget.StopMoving();
        }
        else
        {
            //if (_ownTarget.GetComponent<Villager>())
        }
    }
    
    public virtual void StopActionOnTarget()
    {
        _hasPriorityOnTarget = false;
        _isAttacking = false;
    }

    public void IsAttacked(bool isAttacked)
    {
        _isAttacked = isAttacked;
        if (!isAttacked)
        {
            MoveAgain();
        }
    }

    public virtual void StopMoving()
    {
        _isEscaping = false;
        _agent.speed = 0;
        // Animation get down on the ground
    }

    public virtual void MoveAgain()
    {
        FastSpeed();
    }

    // Remove the item from the list and get a new target
    public void GetNewTarget()
    {
        Debug.Log("new target");
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
        if (_isAttacked)
        {
            StopMoving();
        }
        else
        {
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
            else if (_isMovingAround)
            {
                if (IsDestinationReached(_stoppingDistance))
                {
                    SetRandomDestination(_assignedGroup.RandomNavmeshLocation());
                }
            }
            if (_ownTarget != null && _ownTarget.activeSelf)
            {
                // Update the destination in case the target is moving
                _agent.SetDestination(_ownTarget.transform.position);

                // If we are attacking
                if (_isAttacking)
                {
                    CharacterStats targetStats = _ownTarget.GetComponent<CharacterStats>();

                    if (targetStats != null)
                    {
                        if (targetStats.isAlive)
                        {
                            _combat.Attack(targetStats);
                        }
                    }

                    //StopTarget();
                }

                // If we are regrouping
                else if (_assignedGroup.IsRegrouping())
                {
                    // Stop when one member has join the rally point (the actual target)
                    if (IsDestinationReached(_stoppingDistance))
                    {
                        _assignedGroup.StopRegrouping();
                        _assignedGroup.ShareNoTarget();
                        _assignedGroup.MoveRandom();
                    }
                }
            }
            else
            {
                if (_isAttacking)
                {
                    StopActionOnTarget();
                    MoveAgain();
                    GetNewTarget();
                }
                /*
                if (!_isMovingAround)
                {
                    Debug.Log("JE PASSE PASSE");
                    _assignedGroup.MoveRandom();
                }
                */
            }
        }        
    }

    // Set a Destination (not a target gameobject)
    public void SetRandomDestination(Vector3 destination)
    {
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

    public void Die()
    {
        _assignedGroup.RemoveItem(this.gameObject);
        // Disappear ?
        gameObject.SetActive(false);
    }

    public bool IsTargetRegistered(GameObject target)
    {
        return _assignedGroup.IsTargetRegistered(target);
    }

    public bool HasATarget()
    {
        return _ownTarget != null;
    }
}
