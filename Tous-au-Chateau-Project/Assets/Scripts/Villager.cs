using System.Collections;
using UnityEngine;

public struct Direction { };
public interface MapPhysicObject { };

public class Villager : MonoBehaviour, MapPhysicObject
{
    private bool _isAlive;
    private bool _isInfected;
    private int _life;
    private int _motivation;
    private Direction _mainDirection;

    private bool _canMove;
    public float _speed = 1.50f;
    Rigidbody _rb;

    private CollisionDetection _villagerCollision;
    private DyingVillager _deathmode;

    

    public Villager() : this(100, true, false, new Direction())
    { }
    public Villager(int life, bool alive, bool infected, Direction direction)
    {
        _isAlive = alive;
        _isInfected = infected;
        _life = life;
        _mainDirection = direction;
    }
    
    void Crush()
    {

    }
    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
    }
    public void Turn(Collider direction)
    {
        if (direction.name == "LeftWoodSign")
        {
            transform.Rotate(0, -90, 0);
        }
        else
        {
            transform.Rotate(0, 90, 0);
        }
    }
    private void GetInfected()
    {
        _isInfected = true;
        _speed = 1.3f;
    }
    private void Die()
    {
        _isAlive = false;
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
        if (_isAlive){
            if(_life <= 0)
            {
                Die();
                _isAlive = false;
                _canMove = false;
            }

            if (_villagerCollision.CheckforSign())
                Turn(_villagerCollision.hitInfo.collider);

            if (_villagerCollision.inDanger)
            {
                if (!_villagerCollision.onPlatform)
                {
                    _life -= 1;
                }

            }

        }
            
        

    }
}
