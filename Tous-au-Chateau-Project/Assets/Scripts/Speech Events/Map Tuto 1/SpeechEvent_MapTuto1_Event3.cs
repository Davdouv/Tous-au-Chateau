using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - CRUSHING A TREE

public class SpeechEvent_MapTuto1_Event3 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			return true;
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// When crushing the flickering tree.
		return false;
	}
}
