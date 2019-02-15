using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 6 - FREE THE WAY (optional message)

public class SpeechEvent_MapTuto1_Event6_1 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			// TODO
			if (/*group dies == true*/false) {
				return true;
			}
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// Crushing a tree
		return false;
	}
}
