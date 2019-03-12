using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DeathReason { NOT_DEAD, UNKNOWN, RIVER, VOID, PLAYER, WOLF, GOLEM, DEATH_REASONS_COUNT }

public class CharacterStats : MonoBehaviour
{
    private bool _isAlive;
    public float life = 100;
    public float speed = 1.0f;
    public float strength = 0;

    private float _saveSpeed;
    public DeathReason _deathReason = DeathReason.NOT_DEAD;

    private AudioSource _audioData;
    public List<AudioClip> deathSound;
    public List<AudioClip> fallingSound;

    private void Start()
    {
        _audioData = GetComponent<AudioSource>();
    }

    public CharacterStats():this(true, 100, 1.0f, 0) { }

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
        if (deathSound.Count > 0 || fallingSound.Count > 0)
        {
            if (deathReason == DeathReason.VOID || deathReason == DeathReason.RIVER)
            {
                _audioData.clip = GetRandomClip(fallingSound);
            }
            else
            {
                _audioData.clip = GetRandomClip(deathSound);
            }
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


        // Disable colliders
        if (gameObject.GetComponent<BoxCollider>())
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (gameObject.GetComponent<SphereCollider>())
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
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

    private AudioClip GetRandomClip(List<AudioClip> audioClipList)
    {
        int rand = Random.Range(0, audioClipList.Count);
        return audioClipList[rand];
    }
}
