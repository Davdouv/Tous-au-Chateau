using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class AICharacterAttack : MonoBehaviour {

    public float attackSpeed = 1f;
    private float _attackCooldown = 0f;

    public float attackDelay = .6f;

    private CharacterStats _stats;

    private void Start()
    {
        _stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update () {
        _attackCooldown -= Time.deltaTime;
    }

    public virtual void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            //StartCoroutine(DoDamage(targetStats, attackDelay));
            DoDamage(targetStats);
            _attackCooldown = 1f / attackSpeed;
        }
    }
    
    IEnumerator DoDamage(CharacterStats targetStats, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Do damage
        if (targetStats.isAlive)
        {
            targetStats.TakeDamage(_stats.strength);
            if (!targetStats.isAlive)
            {
                AICharacter character = gameObject.GetComponent<AICharacter>();

                character.StopActionOnTarget();
                character.GetNewTarget();
                character.MoveAgain();
            }
        }
    }

    private void DoDamage(CharacterStats targetStats)
    {
        if (targetStats.isAlive)
        {
            targetStats.TakeDamage(_stats.strength);
            if (!targetStats.isAlive)
            {
                AICharacter character = gameObject.GetComponent<AICharacter>();

                character.StopActionOnTarget();
                character.GetNewTarget();
                character.MoveAgain();
            }
        }
    }
}
