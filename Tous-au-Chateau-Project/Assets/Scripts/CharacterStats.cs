using UnityEngine;
using System.Collections;

public enum DeathReason { NOT_DEAD, UNKNOWN, RIVER, VOID, PLAYER, WOLF, GOLEM, DEATH_REASONS_COUNT }

public class CharacterStats : MonoBehaviour
{
    private bool _isAlive;
    public float life = 100;
    public float speed = 1.50f;
    public float strength = 0;

    private float _saveSpeed;
    public DeathReason _deathReason = DeathReason.NOT_DEAD;

    private AudioSource _audioData;
    public AudioClip deathSound;

    private void Start()
    {
        _audioData = GetComponent<AudioSource>();
    }

    public CharacterStats():this(true, 100, 2.0f, 0) { }

    public CharacterStats(bool live, float vie, float vitesse, float force)
    {
        _isAlive = live;
        life = vie;
        speed = vitesse;
        strength = force;
    }

    public void TakeDamage(float dmg, DeathReason deathReason = DeathReason.UNKNOWN)
    {
        life -= dmg;
        if (life <= 0)
        {
            Die(deathReason);
        }
    }

    private void Die(DeathReason deathReason = DeathReason.UNKNOWN)
    {
        _isAlive = false;
        _deathReason = deathReason;

        // Play sound
        if (deathSound)
        {
            _audioData.clip = deathSound;
            _audioData.Play();
        }

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

    public DeathReason GetDeathReason()
    {
        return _deathReason;
    }

    public void SetDeathReason(DeathReason deathReason)
    {
        _deathReason = deathReason;
    }
}
