using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 6 - FREE THE WAY (optional message)

public class SpeechEvent_MapTuto1_Event6_1 : SpeechEvent {

    public ResourceManager _ResourceManager;

    private int woodBeforeCrushingTree = 0;

    public override bool MustOpen() {
		// Open after previous event is done
		if (previousEvent != null && previousEvent.IsDone()) {
            //at least one villager is dead
            if (currentVillagersGroup.GetNumberOfVillagersAlive() < currentVillagersGroup.GetNumberOfVillagers())
            {
                // Check if at least one has fallen in the void
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.VOID))
                {
                    woodBeforeCrushingTree = _ResourceManager.GetWood();
                    return true;
                }
            }
        }
		return false;
	}

	public override bool MustClose() {
        // Crushing a tree (any tree)
        if (_ResourceManager.GetWood() >= woodBeforeCrushingTree + 10)
        {
            return true;
        }

        return false;
	}
}
