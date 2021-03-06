using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 - END

public class SpeechEvent_MapTuto3_Event5 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done (or previous previous)
		if (previousEvent != null && (previousEvent.IsDone() || previousEvent.previousEvent.IsDone())) {
			if (GameManager.Instance.IsGameWon()) {
					return true;
			}
		}
		return false;
	}

	public override bool MustClose() {
		// Any action from the player
		return hasDoneAction;
	}
}
