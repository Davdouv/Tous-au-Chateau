using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    private bool _isAlive;
    public float life = 100;
    public float speed = 1.50f;
    public float strength = 0;

    private float _saveSpeed;
    
    public CharacterStats():this(true, 100, 2.0f, 0) { }

    public CharacterStats(bool live, float vie, float vitesse, float force)
    {
        _isAlive = live;
        life = vie;
        speed = vitesse;
        strength = force;
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        Debug.Log(gameObject.name + " life = " + life);
        if (life <= 0)
        {
            Debug.Log(gameObject.name + " dead");
            Die();
        }
    }

    private void Die()
    {
        _isAlive = false;
        // AI Behaviour
        if (gameObject.GetComponent<AICharacter>())
        {
            gameObject.GetComponent<AICharacter>().Die();
        }
        // Villager Behaviour
        else if (gameObject.GetComponent<Villager>())
        {
            gameObject.GetComponent<Villager>().Die();
        }
        // Default behaviour
        else
        {
            gameObject.SetActive(false);
        }
    }
    // Setters
    public void SetIsAlive(bool live) { _isAlive = live; }
    // Getters
    public bool IsAlive() { return _isAlive; }

    public void StopMovement()
    {
        _saveSpeed = speed;
        speed = 0;
    }

    public void MoveAgain()
    {
        speed = _saveSpeed;
    }
}
