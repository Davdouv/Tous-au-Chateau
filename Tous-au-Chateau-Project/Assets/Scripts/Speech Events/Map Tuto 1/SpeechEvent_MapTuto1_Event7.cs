using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 7 - END

public class SpeechEvent_MapTuto1_Event7 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
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
