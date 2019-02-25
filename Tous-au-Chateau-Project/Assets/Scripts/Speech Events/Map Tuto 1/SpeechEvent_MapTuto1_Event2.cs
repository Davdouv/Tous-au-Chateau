using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - LET'S GO!

public class SpeechEvent_MapTuto1_Event2 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			return true;
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// When any action is done from the player (any key pressed)
		return false;
	}
}
