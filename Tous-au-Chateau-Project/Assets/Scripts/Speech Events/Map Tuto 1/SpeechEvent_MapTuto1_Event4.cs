using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BUILDING A WOOD BOARD

public class SpeechEvent_MapTuto1_Event4 : SpeechEvent {

	public bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent._isDone) {
			return true;
		}
		return false;
	}

	public bool MustClose() {
		// TO DO
		// Opening the Construction Panel and placing the wood board.
		return false;
	}
}
