using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 6 - FREE THE WAY

public class SpeechEvent_MapTuto1_Event6 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			// TODO
			if (/*group falls into the void == true*/false) {
					return true;
			}
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// Crushing a tree
		// (any tree)
		return false;
	}
}
