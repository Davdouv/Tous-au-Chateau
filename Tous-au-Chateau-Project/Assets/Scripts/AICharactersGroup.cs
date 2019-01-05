using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharactersGroup : MonoBehaviour {

    private List<AICharacter> _aiCharacters;

    private void Awake()
    {
        _aiCharacters = new List<AICharacter>();
    }

    // Get all aiCharacters of the group
    public void Add(AICharacter character)
    {
        _aiCharacters.Add(character);
    }

    public void ShareTarget(GameObject target)
    {
        foreach (AICharacter character in _aiCharacters)
        {
            character.SetTarget(target);
        }
    }

    public void ShareNoTarget()
    {
        foreach (AICharacter character in _aiCharacters)
        {
            character.NoTarget();
        }
    }
}
