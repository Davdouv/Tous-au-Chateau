using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Villager : MonoBehaviour
{
    Rigidbody _rb;
    NavMeshAgent agent;
    private VillagersGroup _group;
    public int _motivation;
    private Collider _hitbox;

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

    private AudioSource _audioData;
    public AudioClip walkingSound;
    private float countDown = 0f;
    private float timeToWait = 0f;
    private bool _facingObstacle = false;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        _villagerCollision = GetComponent<DangerDetection>();
        _stats = GetComponent<CharacterStats>();
        _deathmode = GetComponent<DyingVillager>();
        _hitbox = GetComponent<BoxCollider>();
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
        //Vector3 objectif = GameObject.Find("Objectif").transform.position;
        //transform.LookAt(new Vector3(objectif.x, transform.position.y , objectif.z  ));

        // SOUND
        _audioData = GetComponent<AudioSource>();
        // Set a random Wait time so the group don't play sound at the same time
        if (walkingSound)
        {
            timeToWait += Random.Range(0, 2* walkingSound.length) + walkingSound.length;
        }        
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
        _anim.SetBool("walk", canMove);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Villager" && collision.gameObject.tag != "Wolf")
        {
            _facingObstacle = true;
        }        
    }

    // When facing an obstacle, move left or right
    private void MoveLeftOrRight()
    {
        RaycastHit centerhit, lefthit, righthit;
        Vector3 moveCorrection = Vector3.zero;
        Vector3 centerRaystart = transform.position + new Vector3(0, 0.8f, 0),
            leftRaystart = centerRaystart + new Vector3(-_hitbox.bounds.extents.x - 0.01f, 0, 0), // slightly off so collider does not get stuck in obstacles
            rightRaystart = centerRaystart + new Vector3(_hitbox.bounds.extents.x + 0.01f, 0, 0);

        Debug.DrawLine(centerRaystart, centerRaystart + transform.forward);
        Debug.DrawLine(leftRaystart, leftRaystart + transform.forward);
        Debug.DrawLine(rightRaystart, rightRaystart + transform.forward);

        if (Physics.Raycast(new Ray(centerRaystart, transform.forward), out centerhit, 0.7f))
        {
            float angleOfApproach = Vector3.SignedAngle(transform.forward, centerhit.normal, transform.up);
            if (angleOfApproach >= 0 && angleOfApproach >= 170) // la surface de contact est légèrement penchée sur la gauche
            {
                moveCorrection = Vector3.right;
            }
            else
            {
                if (angleOfApproach < 0 && angleOfApproach <= -170)
                {
                    moveCorrection = Vector3.left;
                }
            }
        }
        else
        {
            if (Physics.Raycast(new Ray(leftRaystart, transform.forward), out lefthit, 0.7f))
            {
                float angleOfApproach = Vector3.SignedAngle(transform.forward, lefthit.normal, transform.up);
                if (angleOfApproach >= 0 && angleOfApproach >= 170) // la surface de contact est légèrement penchée sur la gauche
                {
                    moveCorrection = Vector3.right;
                }
                else
                {
                    if (angleOfApproach < 0 && angleOfApproach <= -170)
                    {
                        moveCorrection = Vector3.left;
                    }
                }
            }
            else
            {
                if (Physics.Raycast(new Ray(rightRaystart, transform.forward), out righthit, 0.7f))
                {
                    float angleOfApproach = Vector3.SignedAngle(transform.forward, righthit.normal, transform.up);
                    if (angleOfApproach >= 0 && angleOfApproach >= 170) // la surface de contact est légèrement penchée sur la gauche
                    {
                        moveCorrection = Vector3.right;
                    }
                    else
                    {
                        if (angleOfApproach < 0 && angleOfApproach <= -170)
                        {
                            moveCorrection = Vector3.left;
                        }
                    }
                }
                else
                {
                    _facingObstacle = false;
                }
            }
        }        

        _rb.MovePosition(transform.position + (transform.forward + moveCorrection) * _stats.speed * Time.deltaTime);
    }

    private void Move()
    {
        /*agent.ResetPath();
        agent.updatePosition = true;
        agent.velocity = transform.forward * 1.00f;
        */
        if (_facingObstacle)
        {
            MoveLeftOrRight();
        }
        else
        {
            _rb.MovePosition(transform.position + (transform.forward) * _stats.speed * Time.deltaTime);
        }  
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

    
    void FixedUpdate()
    {
        if (_stats.IsAlive())
        {
            if (_canMove)
            {
                Move();

                // Play a sound every timeToWait seconds
                if (countDown > timeToWait && walkingSound)
                {
                    countDown = 0;
                    _audioData.clip = walkingSound;
                    _audioData.Play();
                }
                countDown += Time.deltaTime;
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
                        
                        transform.rotation = _isJoining.transform.rotation;

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
