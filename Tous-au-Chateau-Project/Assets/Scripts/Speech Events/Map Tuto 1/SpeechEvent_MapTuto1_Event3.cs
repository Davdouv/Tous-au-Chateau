using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - CRUSHING A TREE

public class SpeechEvent_MapTuto1_Event3 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
            if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            {
                // Check if at least one has been killed by the river
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.RIVER))
                {
                    return true;
                }
            }
        }
		return false;
	}

	public override bool MustClose() {
		// TODO
		// When crushing the flickering tree.
		return false;
	}
}
