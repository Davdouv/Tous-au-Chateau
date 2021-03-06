using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 6 - FREE THE WAY

public class SpeechEvent_MapTuto1_Event6 : SpeechEvent {

    public ResourceManager _ResourceManager;

    private int woodBeforeCrushingTree = 0;

	public override bool MustOpen() {
		// Open after previous event is done
		if (previousEvent != null && previousEvent.IsDone()) {
      return true;
            // if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            // {
            //     // Check if at least one has fallen in the void
            //     if (currentVillagersGroup.IsDeathCausedBy(DeathReason.VOID))
            //     {
            //         woodBeforeCrushingTree = _ResourceManager.GetWood();
            //
            //     }
            // }
		}
		return false;
	}

	public override bool MustClose() {
		// Crushing a tree (any tree)
		if(_ResourceManager.GetWood() >= woodBeforeCrushingTree + 10)
        {
            return true;
        }

		return false;
	}
}
