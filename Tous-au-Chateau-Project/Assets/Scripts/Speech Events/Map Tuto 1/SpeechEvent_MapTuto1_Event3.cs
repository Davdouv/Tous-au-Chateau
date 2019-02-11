using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CRUSHING A TREE

public class SpeechEvent_MapTuto1_Event3 : SpeechEvent {

	public bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent._isDone) {
			return true;
		}
		return false;
	}

	public bool MustClose() {
		// TO DO
		// When crushing the flickering tree.
		return false;
	}
}
