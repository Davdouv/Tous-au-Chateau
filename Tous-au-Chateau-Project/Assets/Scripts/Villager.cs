using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Villager : MonoBehaviour
{
    Rigidbody _rb;
    NavMeshAgent agent;
    private VillagersGroup _group;
    public int _motivation;

    public bool _isInfected;
    public bool _canMove;

    public bool _isPassive;
    public GameObject _isJoining;
    public bool _hasJoined;

    private DangerDetection _villagerCollision;
    public CharacterStats _stats;
    private DyingVillager _deathmode;


    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        _villagerCollision = GetComponent<DangerDetection>();
        _stats = GetComponent<CharacterStats>();
        _deathmode = GetComponent<DyingVillager>();

        _group = (IsPassive()) ? null : transform.parent.gameObject.GetComponent<VillagersGroup>();
        if (_isInfected)
        {
            gameObject.AddComponent<InfectionSpreading>();
        }

        
        agent.enabled = _isPassive;

        _rb.isKinematic = false;
        _rb.freezeRotation = !_isPassive;
        _rb.interpolation = RigidbodyInterpolation.None;
        _canMove = !_isPassive;
        _hasJoined = !_isPassive;
        _isJoining = null;
        Vector3 objectif = 
            GameObject.Find("Objectif").transform.position;
        transform.LookAt(new Vector3(objectif.x, transform.position.y , objectif.z  ));
    }

    public void Crush()
    {

    }
    private void Move()
    {
        /*agent.ResetPath();
        agent.updatePosition = true;
        agent.velocity = transform.forward * 1.00f;
        */

        _rb.MovePosition(transform.position + transform.forward * _stats.speed * Time.deltaTime);

    }
    private void MoveTowardVillager(GameObject target)
    {
        const float THRESHOLD = 2;
        var pos = target.transform.position;
        transform.LookAt(pos);

        //_rb.MovePosition(transform.position + transform.forward * _stats._speed * Time.fixedDeltaTime);
        //print(name + " dst sqr : " + (agent.destination - pos).sqrMagnitude);
        if ((agent.destination - pos).sqrMagnitude > THRESHOLD)
        {
            //print(name + "New destination : " + pos);
            agent.SetDestination(pos);

        }
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

    public CharacterStats GetStats()
    {
        return _stats;
    }

    public void Die()
    {
        //_stats.SetIsAlive( false);
        // _deathmode.isAlive = false;
        if (_group)
        {
            _group.RemoveVillager(this);
        }        
        _deathmode.isAlive = false;
        _stats.SetIsAlive(false);
        _canMove = false;
        _rb.isKinematic = true;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    public bool IsPassive()
    {
        return _isPassive;
    }
    public void JoinIn(GameObject callguy)
    {
        print(name + " wants to join in");
        _isJoining = callguy;
        _isPassive = false;

        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.SetDestination(_isJoining.transform.position);
    }

    void Update()
    {
        if (_canMove)
        {
            Move();
        }

        if (!_hasJoined)
        {
            if (_isJoining)
            {
                MoveTowardVillager(_isJoining);
                if ((_isJoining.transform.position - transform.position).sqrMagnitude <= 4.0f)
                {
                    print(name + " has joined in");
                    _hasJoined = true;
                    _canMove = true;
                    _rb.freezeRotation = true;

                    _group = _isJoining.GetComponent<Villager>()._group;
                    _group.AddVillagers(GetComponent<Villager>());
                    transform.parent = _group.gameObject.transform;

                    //transform.LookAt(transform.position + _isJoining.transform.forward - _isJoining.transform.position);

                    // print("rotation" +_isJoining.transform.rotation.y);
                    transform.rotation = _isJoining.transform.rotation;
                    //print("rotation" + transform.rotation.y);

                    agent.ResetPath();
                    agent.enabled = false;

                    _isJoining = null;
                }
            }
        }
    }

    // Update is called once per frame
    /*
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
            

        }

        



    }*/
}
