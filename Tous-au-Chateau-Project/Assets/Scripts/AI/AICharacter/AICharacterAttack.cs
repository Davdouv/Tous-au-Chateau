﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AICharacterAttack : MonoBehaviour {

    public float attackSpeed = 1f;
    private float _attackCooldown = 0f;

    public float attackDelay = .6f;
    public DeathReason aiType;

    private CharacterStats _stats;
    private AudioSource _audioData;
    public AudioClip attackSound;

    private bool playSound = true;

    private void Start()
    {
        _stats = GetComponent<CharacterStats>();
        _audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        _attackCooldown -= Time.deltaTime;
    }

    public virtual void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            if (playSound)
            {
                _audioData.clip = attackSound;
                _audioData.Play();
                playSound = false;
            }
            else
            {
                playSound = true;
            }
            //StartCoroutine(DoDamage(targetStats, attackDelay));
            DoDamage(targetStats);
            _attackCooldown = 1f / attackSpeed;
        }
    }
    
    // Not used for now
    /*
    IEnumerator DoDamage(CharacterStats targetStats, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Do damage
        if (targetStats.IsAlive())
        {
            targetStats.TakeDamage(_stats.strength, aiType);
            if (!targetStats.IsAlive())
            {
                AICharacter character = gameObject.GetComponent<AICharacter>();

                character.StopActionOnTarget();
                character.GetNewTarget();
                character.MoveAgain();
            }
        }
    }
    */

    private void DoDamage(CharacterStats targetStats)
    {
        if (targetStats.IsAlive())
        {
            targetStats.TakeDamage(_stats.strength, aiType);
            if (!targetStats.IsAlive())
            {
                AICharacter character = gameObject.GetComponent<AICharacter>();

                character.StopActionOnTarget();
                character.GetNewTarget();
                character.MoveAgain();
            }
        }
    }
}
