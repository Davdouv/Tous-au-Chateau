using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - CRUSHING A TREE

public class SpeechEvent_MapTuto1_Event3 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (previousEvent != null && previousEvent.IsDone()) {
            if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            {
								Debug.Log("Death Group");
                // Check if at least one has been killed by the river
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.RIVER))
                {
										Debug.Log("Death River");
                    VillagersManager.Instance.SpawnGroup();
                    return true;
                }
            }
        }
		return false;
	}

	public override bool MustClose() {
		// When crushing the flickering tree.
    if (MapManager.Instance.firstTree != null && ResourceManager.Instance.GetWood() > 0)
    {
      return true;
    }
		return false;
	}
}
