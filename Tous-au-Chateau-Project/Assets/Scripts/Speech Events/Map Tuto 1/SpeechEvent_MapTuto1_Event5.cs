using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5 - CRUSHING AN ENNEMY

public class SpeechEvent_MapTuto1_Event5 : SpeechEvent {

	public override bool MustOpen() {
		// Open after previous event is done
		if (_previousEvent != null && _previousEvent.IsDone()) {
			// TODO
			if (/*group gets eaten by a wolf == true*/false) {
                Debug.Log("Villagers are dead !");
					return true;
			}
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// Crush the wolf
		return false;
	}
}
