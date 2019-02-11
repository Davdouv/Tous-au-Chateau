using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 - CRUSHING AN ENNEMY

public class SpeechEvent_MapTuto1_Event5 : SpeechEvent {

	public override bool MustOpen() {
		// TODO
		if (/*group gets eaten by a wolf == true*/false) {
				return true;
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// Crush the wolf
		return false;
	}
}
