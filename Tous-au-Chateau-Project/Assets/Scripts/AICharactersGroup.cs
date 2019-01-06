using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharactersGroup : MonoBehaviour {

    // Children objects of the group
    private List<AICharacter> _aiCharacters;
    // List of all targets spotted by the group
    private List<GameObject> _targetDetected;
    
    // RallyPoint, move along with the group
    private GameObject _rallyPoint;
    private NavMeshAgent _rallyPointAgent;

    // Speed of the group
    public float slowSpeed = 2.0f;
    public float fastSpeed = 3.5f;

    private bool _isGroupMoving;
    private bool _regrouping;

    private void Awake()
    {
        _aiCharacters = new List<AICharacter>();
        _targetDetected = new List<GameObject>();
    }

    private void Start()
    {
        // Get the children into a list
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                AICharacter aiCharacter = child.GetComponent<AICharacter>();
                AddCharacter(aiCharacter);
                aiCharacter.SetSlowSpeed(slowSpeed);
                aiCharacter.SetFastSpeed(fastSpeed);
            }            
        }
        CreateRallyPoint();
        MoveRandom();
        //Regroup();
    }

    private void CreateRallyPoint()
    {
        _rallyPoint = new GameObject("RallyPoint");
        _rallyPoint.transform.position = transform.position;
        _rallyPoint.transform.rotation = transform.rotation;
        _rallyPoint.transform.SetParent(transform);
        _rallyPointAgent = _rallyPoint.AddComponent<NavMeshAgent>();
        _rallyPointAgent.enabled = true;
        _rallyPointAgent.Warp(_rallyPoint.transform.position);
        _rallyPointAgent.speed = slowSpeed;
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
        if (_isGroupMoving)
        {
            StopMoveRandom();
        }
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
        if (_aiCharacters.TrueForAll(character => character.gameObject.GetComponent<AIDetection>().IsInRange(target.transform.position) == false))
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

    // Make the rallyPoint the new destination of all the aiCharacters
    private void Regroup()
    {
        _regrouping = true;
        ShareTarget(_rallyPoint);
    }

    public bool IsRegrouping()
    {
        return _regrouping;
    }

    public void StopRegrouping()
    {
        _regrouping = false;
    }

    // Move the group to a random location
    public void MoveRandom()
    {
        _isGroupMoving = true;
        _rallyPointAgent.isStopped = false;
        float range = 20f;
        Vector3 destination = RandomNavmeshLocation(range);
        _rallyPointAgent.SetDestination(destination);
        ShareDestination(destination);
    }

    // Get a random location on the navmesh
    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection.y = 0;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        // Finds the closest point on NavMesh within specified range.
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Send a common target
    public void ShareDestination(Vector3 destination)
    {
        Vector3 distanceFromRallyPoint;
        Vector3 characterDestination;
        foreach (AICharacter character in _aiCharacters)
        {
            distanceFromRallyPoint = character.transform.position - _rallyPoint.transform.position;
            characterDestination = destination + distanceFromRallyPoint;
            character.SetRandomDestination(characterDestination);
        }
    }

    // Stop the group from moving. Called when a target is detected
    private void StopMoveRandom()
    {
        _isGroupMoving = false;
        _rallyPointAgent.isStopped = true;
        ShareNoTarget();
    }

    // Check if RallyPoint has reached destination
    private bool IsDestinationReached()
    {
        float stoppingDistance = 0.5f;
        return (Vector3.Distance(_rallyPoint.transform.position, _rallyPointAgent.destination) < stoppingDistance);
    }

    public GameObject GetRallyPoint()
    {
        return _rallyPoint;
    }

    private void Update()
    {
        if (_isGroupMoving)
        {
            // Check if destination is reached
            if (IsDestinationReached())
            {
                // Chose another destination
                MoveRandom();
            }
        }
    }
}
