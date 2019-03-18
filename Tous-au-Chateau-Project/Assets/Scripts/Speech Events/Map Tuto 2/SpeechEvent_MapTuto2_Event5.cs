using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 - END

public class SpeechEvent_MapTuto2_Event5 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (previousEvent != null && previousEvent.IsDone()) {
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
