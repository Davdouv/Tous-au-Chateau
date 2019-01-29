using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public bool isAlive;
    public float life;
    public float speed = 1.50f;
    public float strength;
    
    public CharacterStats():this(true, 100, 1.50f, 0) { }

    public CharacterStats(bool live, float vie, float vitesse, float force)
    {
        isAlive = live;
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
}
