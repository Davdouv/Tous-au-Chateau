using System.Collections;
using UnityEngine;

public class Villager : MapPhysicObject
{
    
    private int _motivation;
    private bool _isInfected;
    private bool _canMove;
    private CharacterStats _stats;
    Rigidbody _rb;

    private CollisionDetection _villagerCollision;
    private DyingVillager _deathmode;

    

    public Villager() : this( false)
    { }
    public Villager(bool infected)
    {
        _isInfected = infected;
        _stats = new CharacterStats();
    }
    
    public override void Crush()
    {

    }
    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _stats.speed * Time.fixedDeltaTime);
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
    
    private void GetInfected()
    {
        _isInfected = true;
        _stats.speed = 1.3f;
    }
    private void Die()
    {
        _stats.isAlive = false;
        _deathmode.isAlive = false;
        // delete villager ?
        // callback on villagersgroup to erase from list ?
    }
    
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _villagerCollision = GetComponent<CollisionDetection>();
        _deathmode = GetComponent<DyingVillager>();
        transform.LookAt(GameObject.Find("Objectif").transform.position);
        _rb.freezeRotation = true;
        _rb.isKinematic = true;
        _canMove = true;
    }
    void FixedUpdate()
    {
        if (_canMove)
            Move();

    }
    // Update is called once per frame
    void Update()
    {
        if (_stats.isAlive){
            if(_stats.life <= 0)
            {
                Die();
                _stats.isAlive = false;
                _canMove = false;
            }

            
            if (_villagerCollision.inDanger)
            {
                if (!_villagerCollision.onPlatform)
                {
                    _stats.life -= 1;
                }

            }

        }
            
        

    }
}
