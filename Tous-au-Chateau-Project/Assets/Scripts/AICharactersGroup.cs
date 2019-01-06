using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharactersGroup : MonoBehaviour {

    private List<AICharacter> _aiCharacters;
    private List<GameObject> _targetDetected;

    private void Awake()
    {
        _aiCharacters = new List<AICharacter>();
        _targetDetected = new List<GameObject>();
    }

    // All aiCharacters of the group will add themselves to the list
    public void AddCharacter(AICharacter character)
    {
        _aiCharacters.Add(character);
    }

    public void AddTarget(GameObject target)
    {
        // Make sur we don't have listed this target already
        if (!_targetDetected.Contains(target))
        {
            _targetDetected.Add(target);
        }
    }

    // Send a common target
    public void ShareTarget(GameObject target)
    {
        foreach (AICharacter character in _aiCharacters)
        {
            character.SetTarget(target);
        }
    }

    // Send no target
    public void ShareNoTarget()
    {
        foreach (AICharacter character in _aiCharacters)
        {
            character.NoTarget();
        }
    }

    // Change target for all aiCharacters except for the one asking
    public void ChangeTarget(GameObject target, AICharacter aiCharacter)
    {
        // First check if there are other targets available
        if (_targetDetected.Count < 2)
        {
            return;
        }

        // Find a new target
        GameObject newTarget = _targetDetected.Find(obj => obj != target);

        // Set the new target to other characters
        int index = _aiCharacters.IndexOf(aiCharacter);
        for (int i = 0; i < _aiCharacters.Count; ++i)
        {
            if (i != index)
            {
                _aiCharacters[i].NoTarget();
                _aiCharacters[i].SetTarget(newTarget);
            }
        }
    }

    // Cancel target for all aiCharacters who were targeting it
    public void CancelTarget(GameObject target)
    {
        foreach (AICharacter character in _aiCharacters)
        {
            if (character.IsTheTarget(target))
            {
                character.NoTarget();
            }
        }
    }

    // For each aiCharacters, test if the target is still is sight or not
    public void CheckIfRemoveTarget(GameObject target)
    {
        if (_aiCharacters.TrueForAll(character => character.gameObject.GetComponent<AIDetection>().IsInSight(target) == false))
        {
            _targetDetected.Remove(target);
        }
    }

    // Send a new target if there's one on the list
    public void NewTarget()
    {
        if (_targetDetected.Count == 0)
        {
            ShareNoTarget();
            Regroup();
        }
        else
        {
            ShareTarget(_targetDetected[0]);
        }
    }

    public void RemoveItem(GameObject item)
    {
        _targetDetected.Remove(item);
    }

    public void Regroup()
    {
        ShareTarget(this.gameObject);
    }
}
