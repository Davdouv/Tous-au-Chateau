using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - CRUSHING A TREE

public class SpeechEvent_MapTuto1_Event3 : SpeechEvent {

    public GameObject ArrowToDisplay;
    bool stopWait = false;

	public override bool MustOpen() {
		// Open after previous event is done
		if (previousEvent != null && previousEvent.IsDone())
        {
		    if (AreAllVillagersDead() &&
                currentVillagersGroup.IsDeathCausedBy(DeathReason.RIVER))
			{
                ArrowToDisplay.SetActive(true);
                return true;
			}
        }
		return false;
	}

	public override bool MustClose()
    {
		// When crushing the flickering tree. // Destroying the firstTree will set it to null
        if (ResourceManager.Instance.GetWood() > 0)
        {
            StartCoroutine(WaitSeconds());
            ArrowToDisplay.SetActive(false);            
            return stopWait;
        }
		return false;
	}

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        stopWait = true;
    }
}
