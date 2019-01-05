using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharactersGroup : MonoBehaviour {

    private List<AICharacter> _aiCharacters;
    private Transform _currentTarget;

    private void Awake()
    {
        _aiCharacters = new List<AICharacter>();
        _currentTarget = null;
    }

    // Get all aiCharacters of the group
    public void Add(AICharacter character)
    {
        _aiCharacters.Add(character);
    }

    // Give the target to everyone in the group only. Target must be null before
    public void ShareTarget(Transform target)
    {
        // If Cancel Target
        if (target == null)
        {
            _currentTarget = null;
        }
        if (_currentTarget == null)
        {
            _currentTarget = target;
            foreach (AICharacter character in _aiCharacters)
            {
                character.SetTarget(target);
            }
        }
    }
}
