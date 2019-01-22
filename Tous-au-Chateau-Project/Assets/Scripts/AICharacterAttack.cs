using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO --> to get Dmg
//[RequireComponent(typeof(CharacterStats))]
public class AICharacterAttack : MonoBehaviour {

    public float attackSpeed = 1f;
    private float _attackCooldown = 0f;

    public float attackDelay = .6f;
	
	// Update is called once per frame
	void Update () {
        _attackCooldown -= Time.deltaTime;
    }

    public virtual void Attack()
    {
        if (_attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(attackDelay));
            _attackCooldown = 1f / attackSpeed;
        }
    }

    // TODO --> Get Dmg
    IEnumerator DoDamage(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Do damage
        Debug.Log("Attack");
    }
}
