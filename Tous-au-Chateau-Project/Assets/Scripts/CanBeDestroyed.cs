using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeDestroyed : MonoBehaviour {

    // List of object that has triggered this object
    public List<TriggerZone> hasBeenTrigered;

    // Use this for initialization
    void Start () {
        hasBeenTrigered = new List<TriggerZone>();
    }

    private void OnDestroy()
    {
        hasBeenTrigered.ForEach(triggerZone => triggerZone.RemoveTarget(this.gameObject));
    }
}
