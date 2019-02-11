using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LET'S GO!

public class SpeechEvent_MapTuto1_Event2 : SpeechEvent {

	public bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent._isDone) {
			return true;
		}
		return false;
	}

	public bool MustClose() {
		// TO DO
		// When any action is done from the player (any key pressed)
		return false;
	}
}
