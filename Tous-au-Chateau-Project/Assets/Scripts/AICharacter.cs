using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This should be used on an AI Object, child of an object with the AICharactersGroup component
public class AICharacter : EnvironmentMaterial {
    
    private bool _isAlive;
    private NavMeshAgent _agent;
    private AICharactersGroup _assignedGroup;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetGroup();
    }

    // Get the group and add him to it
    private void SetGroup()
    {
        _assignedGroup = transform.parent.GetComponent<AICharactersGroup>();
        _assignedGroup.Add(GetComponent<AICharacter>());
    }

    // If target found, ask the group to share the target with all group objects
    public void TargetFound(Transform target)
    {
        _assignedGroup.ShareTarget(target);
    }

    // Used by the AICharactersGroup
    public void SetTarget(Transform target)
    {
        _agent.SetDestination(target.position);
    }
}
