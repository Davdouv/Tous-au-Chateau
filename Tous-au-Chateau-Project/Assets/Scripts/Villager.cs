using System.Collections;
using UnityEngine;

public enum Direction { RIGHT, LEFT, BACKWARD };


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
        if (infected)
        {
            gameObject.AddComponent<InfectionSpreading>();
        }
        _isInfected = infected;
        _stats = new CharacterStats();
    }
    
    public override void Crush()
    {

    }
    private void Move()
    {
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
    
    private void GetInfected()
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
            
        

    }
}
