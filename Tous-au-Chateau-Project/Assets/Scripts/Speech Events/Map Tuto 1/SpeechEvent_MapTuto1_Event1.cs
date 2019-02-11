using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1 - INTRODUCTION

public class SpeechEvent_MapTuto1_Event1 : SpeechEvent {
	private bool _hasOpenedAlready = false;

	public override bool MustOpen() {
		Debug.Log("mustOpen");
		// Open automatically
		if (!_hasOpenedAlready) {
			_hasOpenedAlready = true;
			return true;
		}
		return false;
	}

	public override bool MustClose() {
		// TODO
		// When crushing the ground near the People.
		return false;
	}
}
