using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4 - BUILDING A WOOD BOARD

public class SpeechEvent_MapTuto1_Event4 : SpeechEvent
{
    // TODO --> Need to set the currentBuilding somewhere
    //public WoodPlank currentBuilding;
    public bool hasWookPlankLanded = false;

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			return true;
		}
		return false;
	}

	public override bool MustClose() {
        // TODO
        // Opening the Construction Panel and placing the wood board
        // (not necessary at the right place, could be anywhere)
        //if (currentBuilding.HasLanded())
        return hasWookPlankLanded;
	}
}
