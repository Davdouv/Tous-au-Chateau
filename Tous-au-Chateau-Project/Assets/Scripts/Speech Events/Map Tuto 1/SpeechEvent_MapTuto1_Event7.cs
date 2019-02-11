using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 7 - END

public class SpeechEvent_MapTuto1_Event7 : SpeechEvent {

	public override bool MustOpen() {
		// TO DO
		if (/*group enters the house == true*/false) {
				return true;
		}
		return false;
	}

	public override bool MustClose() {
		// TO DO
		// Any action from the player
		return false;
	}
}
