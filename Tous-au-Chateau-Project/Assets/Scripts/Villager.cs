using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum Direction { RIGHT, LEFT, BACKWARD };


public class Villager : MapPhysicObject
{
    Rigidbody _rb;
    public VillagersGroup _group;
    public int _motivation;
    public CharacterStats _stats;

    public bool _isInfected;
    public bool _canMove;

    public bool _isPassive;
    public GameObject _isJoining;
    public bool _hasJoined;

    private CollisionDetection _villagerCollision;
    private DyingVillager _deathmode;
    

    

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _villagerCollision = GetComponent<CollisionDetection>();
        _deathmode = GetComponent<DyingVillager>();
        
        _group = (IsPassive()) ? null : transform.parent.gameObject.GetComponent<VillagersGroup>();
        _stats = new CharacterStats();
        if (_isInfected)
        {
            gameObject.AddComponent<InfectionSpreading>();
        }



        _rb.isKinematic = false;
        _rb.freezeRotation = !_isPassive;
        _canMove = !_isPassive;
        _hasJoined = !_isPassive;
        _isJoining = null;
        transform.LookAt(GameObject.Find("Objectif").transform.position);
    }

    public override void Crush()
    {

    }
    private void Move()
    {
        if (_isPassive)
            print(" IM MOVING BUT IM PASSIVE WTFFFFFFFFFFFFFF");
        _rb.MovePosition(transform.position + transform.forward * _stats._speed * Time.fixedDeltaTime);
    }
    public void ChangeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.BACKWARD:
                transform.Rotate(0, 180, 0);
                break;
            case Direction.LEFT:
                transform.Rotate(0, -90, 0);
                break;
            case Direction.RIGHT:
                transform.Rotate(0, 90, 0);
                break;

            default:
                break;
            
                
        }
    }
    
    public void GetInfected()
    {
        _isInfected = true;
        gameObject.AddComponent<InfectionSpreading>();
    }
    private void Die()
    {
        _stats.SetIsAlive( false);
        _deathmode.isAlive = false;
        // delete villager ?
        // callback on villagersgroup to erase from list ?
    }
    public bool IsPassive()
    {
        return _isPassive;
    }
    public void JoinIn(GameObject callguy)
    {
        print(name +" wants to join in");
        _isJoining = callguy;
        _isPassive = false;
        // movement flag don't activate
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_isJoining.transform.position);
    }

    
    void FixedUpdate()
    {
        if (_canMove)
            Move();
        
            
    }
    // Update is called once per frame
    void Update()
    {
        if (_stats.GetIsAlive()){
            if(_stats.GetLife() <= 0)
            {
                Die();
                _stats.SetIsAlive(false);
                _canMove = false;
            }

            
            if (_villagerCollision.inDanger)
            {
                if (!_villagerCollision.onPlatform)
                {
                    _stats.SetLife(_stats.GetLife()-1);
                }

            }
            if (!_hasJoined)
            {
                if (_isJoining)
                {
                    NavMeshAgent agent = GetComponent<NavMeshAgent>();
                    agent.ResetPath();
                    agent.SetDestination(_isJoining.transform.position);
                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        print(name + " has joined in");
                        _hasJoined = true;
                        _canMove = true;
                        _rb.freezeRotation = true;
                        transform.LookAt(transform.position + _isJoining.transform.forward - _isJoining.transform.position);
                        
                        _group = _isJoining.transform.parent.gameObject.GetComponent<VillagersGroup>();
                        _group.AddVillagers(GetComponent<Villager>());
                        transform.parent = _group.gameObject.transform;
                        _isJoining = null;

                        agent.ResetPath();
                    }
                }

            }

        }

        



    }
}
