using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be used on an AI Object with the AICharacter script
public class AIMovement : TriggerZone {

    private AICharacter _aiCharacter;

    private void Start()
    {
        _aiCharacter = this.transform.GetComponent<AICharacter>();
    }

    public override void TriggerEnter(Collider other)
    {
        _aiCharacter.TargetFound(other.transform);
    }


}
