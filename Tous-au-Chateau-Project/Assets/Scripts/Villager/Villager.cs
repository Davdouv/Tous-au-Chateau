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
    [SerializeField]
    private bool _canMove;

    private bool _canTurn = true;
    private const float _timeToWaitToTurnAgain = 2f;
    private float _timePassed = 0f;

    public bool _isPassive;
    public GameObject _isJoining;
    public bool _hasJoined;
    private bool _hasReachedObjectif;

    private DangerDetection _villagerCollision;
    public CharacterStats _stats;
    private DyingVillager _deathmode;

    private Animator _anim;    

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        _villagerCollision = GetComponent<DangerDetection>();
        _stats = GetComponent<CharacterStats>();
        _deathmode = GetComponent<DyingVillager>();

        _anim = transform.GetChild(0).gameObject.GetComponent<Animator>();

        if (!IsPassive() && _canMove)
        {
            _anim.SetBool("walk", true);
        }

        _group = (IsPassive()) ? null : transform.parent.gameObject.GetComponent<VillagersGroup>();
        if (_group)
        {
            _group.AddVillagers(this);
        }
        if (_isInfected)
        {
            gameObject.AddComponent<InfectionSpreading>();
        }


        //agent.enabled = _isPassive;
        agent.enabled = false;  // Set active only when we need it active

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

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
        _anim.SetBool("walk", canMove);
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
        if (_stats.IsAlive() && _canTurn)
        {
            switch (dir)
            {
                /*
                case Direction.BACKWARD:
                    transform.Rotate(0, 180, 0);
                    break;
                */
                case Direction.LEFT:
                    transform.Rotate(0, -90, 0);
                    break;
                case Direction.RIGHT:
                    transform.Rotate(0, 90, 0);
                    break;

                default:
                    break;
            }
            _canTurn = false;
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
        /*
        if (_group)
        {
            _group.RemoveVillager(this);
        }  
        */
        _anim.SetBool("death", true);
        _deathmode.isAlive = false;
        _stats.SetIsAlive(false);
        _canMove = false;
        _rb.isKinematic = true;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        // Test if it was the last villager
        if (VillagersManager.Instance.HasLastVillagersReachedObjectif())
        {
            Debug.Log("VICTORY");
            GameManager.Instance.GameWon(Victory.scoreCount);
        }
    }
    public bool IsPassive()
    {
        return _isPassive;
    }
    public void JoinIn(GameObject callguy)
    {
        if (_stats.IsAlive())
        {
            _isJoining = callguy;
            _isPassive = false;

            agent.enabled = true;
            agent.updatePosition = true;
            agent.updateRotation = true;
            agent.SetDestination(_isJoining.transform.position);
            _anim.SetBool("walk", true);
        }
    }

    void Update()
    {
        if (_stats.IsAlive())
        {
            if (_canMove)
            {
                Move();
            }

            // To avoid getting the effect of a directionnal pannel multiple times in a row
            if (!_canTurn)
            {
                _timePassed += Time.deltaTime;
                if (_timePassed >= _timeToWaitToTurnAgain)
                {
                    _timePassed = 0;
                    _canTurn = true;
                }
            }

            // For villager joining the group
            if (!_hasJoined)
            {
                if (_isJoining)
                {
                    MoveTowardVillager(_isJoining);
                    if ((_isJoining.transform.position - transform.position).sqrMagnitude <= 4.0f)
                    {
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
    }

    public void SetHasReachedObjectif()
    {
        _hasReachedObjectif = true;

        // Not sure if it's good here
        _canMove = false;
        //GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        _rb.isKinematic = true;
    }

    public bool HasReachedObjectif()
    {
        return _hasReachedObjectif;
    }

    public VillagersGroup GetGroup()
    {
        return _group;
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
