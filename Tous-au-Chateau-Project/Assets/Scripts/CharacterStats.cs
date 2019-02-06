using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    private bool _isAlive;
    private int _life;
    private float _speed = 1.50f;
    private float _strength;
    
    public CharacterStats():this(true, 100, 2.0f, 0) { }

    public CharacterStats(bool live, float vie, float vitesse, float force)
    {
        _isAlive = live;
        _life = vie;
        _speed = vitesse;
        _strength = force;
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        //Debug.Log(gameObject.name + " life = " + life);
        if (life <= 0)
        {
            Debug.Log(gameObject.name + " dead");
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        // AI Behaviour
        if (gameObject.GetComponent<AICharacter>())
        {
            gameObject.GetComponent<AICharacter>().Die();
        }
        // Villager Behaviour
        else if (gameObject.GetComponent<Villager>())
        {
            gameObject.GetComponent<AICharacter>().Die();
        }
        // Default behaviour
        else
        {
            gameObject.SetActive(false);
        }
    }
    // Setters
    public void SetIsAlive(bool live) { _isAlive = live; }
    public void SetLife(int vie) { _life = vie; }
    public void SetSpeed(float vitesse) { _speed = vitesse; }
    public void SetStrength(float force) { _strength = force; }
    // Getters
    public bool GetIsAlive() { return _isAlive; }
    public int GetLife() { return _life; }
    public float GetSpeed() { return _speed; }
    public float GetStrength() { return _strength; }
}
