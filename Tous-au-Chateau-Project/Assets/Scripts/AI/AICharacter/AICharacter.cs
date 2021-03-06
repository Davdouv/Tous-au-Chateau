﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
[RequireComponent(typeof(AICharacterAttack))]
public class AICharacter : EnvironmentMaterial {
    
    // Components attached to object
    private NavMeshAgent _agent;
    private AICharacterAttack _combat;
    private CharacterStats _stats;

    private AICharactersGroup _assignedGroup;

    // Target & destination for navMesh agent
    private GameObject _ownTarget;
    private Vector3 _destination;

    private float _stoppingDistance = 1.5f;

    // States
    private bool _hasPriorityOnTarget = false;
    private bool _isEscaping = false;
    private bool _isMovingAround = false;
    private bool _isAttacking = false;
    private bool _isAttacked = false;

    private AudioSource _audioData;
    public AudioClip idleSound;
    public AudioClip warningSound;
    private float countDown = 0f;
    private float timeToWait = 10f;

    private Animator _anim;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stats = GetComponent<CharacterStats>();
        _audioData = GetComponent<AudioSource>();
        _anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        _anim.SetBool("walk", true);
    }

    private void Start()
    {        
        _combat = GetComponent<AICharacterAttack>();
        _assignedGroup = transform.parent.GetComponent<AICharactersGroup>();

        // Set a random Wait time so the group don't play sound at the same time
        timeToWait += Random.Range(5, 30) + idleSound.length;
    }

    // Get the group and add him to it
    public bool IsAttacking()
    {
        return _isAttacking;
    }

    // If target found, ask the group to share the target with all group objects
    public void TargetFound(GameObject target)
    {
        Villager _targetVillager = target.GetComponent<Villager>();
        if (_targetVillager && _targetVillager.HasReachedObjectif())
        {
            return;
        }
        CharacterStats _targetStats = target.GetComponent<CharacterStats>();
        if (_targetStats && _targetStats.IsAlive())
        {
            _hasPriorityOnTarget = true;
            _assignedGroup.AddTarget(target);
            _assignedGroup.ShareTarget(target);

            // If we have a warning sound and it's a villager, and it's the first villager detected
            if (warningSound && _targetVillager && _assignedGroup.CountTargetRegistered() == 1)
            {
                _audioData.clip = warningSound;
                _audioData.Play();
            }
        }        
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
        if (_stats.IsAlive())
        {            
            if ((_ownTarget == null && !_isEscaping) || (_ownTarget != null && !_hasPriorityOnTarget)) // No Target before OR didn't have priority on target
            {
                _ownTarget = target;
                if (_ownTarget) // Make sure target is not null
                {
                    SetFastSpeed();
                    SetDestination(target.transform.position);
                }
            }
        }
    }

    // Set no target
    public void NoTarget()
    {
        _hasPriorityOnTarget = false;
        _ownTarget = null;
        _destination = _assignedGroup.GetRallyPoint().transform.position;
        SetNormalSpeed();
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
        /*
        AICharacter aiTarget = _ownTarget.GetComponent<AICharacter>();
        if (aiTarget)
        {
            aiTarget.StopMoving();
        }
        else
        {
            if (_ownTarget.GetComponent<Villager>())
        }
        */
        CharacterStats targetStats = _ownTarget.GetComponent<CharacterStats>();
        if (targetStats)
        {
            targetStats.StopMovement();
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
        _anim.SetBool("walk", false);
        // Animation get down on the ground
    }

    public virtual void MoveAgain()
    {
        SetFastSpeed();
        _anim.SetBool("walk", true);
    }

    // Remove the item from the list and get a new target
    public void GetNewTarget()
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
        //return (Vector3.Distance(_destination, transform.position) < stoppingDistance);
        return ((_destination - transform.position).sqrMagnitude < stoppingDistance * stoppingDistance);
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
            // AI CHARACTER ESCAPING
            if (_isEscaping)
            {
                GameObject ennemy = GetComponent<AIDetection>().GetEnemyNear();
                float dist = GetComponent<AIDetection>().distanceDetection;
                // If there's still an ennemy close to us
                if (ennemy && ((ennemy.transform.position - transform.position).sqrMagnitude < dist*dist))
                {
                    EscapeFrom(ennemy);
                }
                else
                {
                    StopEscaping();
                }
            }
            // AI CHARACTER SAFE, MOVING AROUND
            else if (_isMovingAround)
            {  
                if (IsDestinationReached(_stoppingDistance))
                {
                    SetRandomDestination(_assignedGroup.RandomNavmeshLocation());
                }

                // Play a sound every timeToWait seconds
                if (countDown > timeToWait)
                {
                    countDown = 0;
                    _audioData.clip = idleSound;
                    _audioData.Play();
                }
                countDown += Time.deltaTime;
            }
            // AI CHARACTER CHASING A TARGET
            if (_ownTarget != null && _ownTarget.activeSelf)
            {
                // If it's a villager, make sur he didn't reached the castle
                if (_ownTarget.GetComponent<Villager>())
                {
                    if (_ownTarget.GetComponent<Villager>().HasReachedObjectif())
                    {
                        // Find another target
                        StopActionOnTarget();
                        MoveAgain();
                        GetNewTarget();

                        // Stop algorithm here
                        return;
                    }
                }

                // Update the destination in case the target is moving
                _agent.SetDestination(_ownTarget.transform.position);

                // If we are attacking
                if (_isAttacking)
                {
                    CharacterStats targetStats = _ownTarget.GetComponent<CharacterStats>();

                    if (targetStats != null)
                    {
                        if (targetStats.IsAlive())
                        {
                            _combat.Attack(targetStats);
                            FaceTarget();
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
                // If he was attacking but has no longer a target to attack
                if (_isAttacking)
                {
                    StopActionOnTarget();
                    MoveAgain();
                    GetNewTarget();
                }
            }
        }        
    }

    // Set a Destination (not a target gameobject)
    public void SetRandomDestination(Vector3 destination)
    {
        SetNormalSpeed();
        SetDestination(destination);
    }

    public float GetSpeed()
    {
        return _stats.speed;
    }

    public void SetNormalSpeed()
    {
        _agent.speed = GetComponent<CharacterStats>().speed;
    }

    public void SetFastSpeed()
    {
        _agent.speed = GetComponent<CharacterStats>().speed * 2;
    }

    // Go away from the enemy
    public void EscapeFrom(GameObject enemy)
    {
        _isEscaping = true;
        _ownTarget = null;
        SetFastSpeed();
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
        _audioData.Stop();
        _anim.SetBool("death", true);
        _assignedGroup.RemoveItem(this.gameObject);
        _agent.SetDestination(this.transform.position);
        _ownTarget = this.gameObject;
        _isMovingAround = false;
        _isEscaping = false;
        _isAttacking = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        // Disappear ?
        //gameObject.SetActive(false);
    }

    public bool IsTargetRegistered(GameObject target)
    {
        return _assignedGroup.IsTargetRegistered(target);
    }

    public bool HasATarget()
    {
        return _ownTarget != null;
    }

    public CharacterStats GetStats()
    {
        return _stats;
    }

    // Turn the object smoothly so it faces the target
    private void FaceTarget()
    {
        if (this.transform.rotation != _ownTarget.transform.rotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_ownTarget.transform.position - transform.position);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, 2 * Time.deltaTime);
        }
    }
}
