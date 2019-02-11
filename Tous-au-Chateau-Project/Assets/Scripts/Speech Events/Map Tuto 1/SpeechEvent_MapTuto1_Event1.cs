using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INTRODUCTION

public class SpeechEvent_MapTuto1_Event1 : SpeechEvent {
	private bool _hasOpenedAlready = false;

	void Start() {
		_hasOpenedAlready = _isOpen;
	}

	public bool MustOpen() {
		// Open automatically
		if (!_hasOpenedAlready) {
			_hasOpenedAlready = true;
			return true;
		}
		return false;
	}

	public bool MustClose() {
		// TO DO
		// When crushing the ground near the People.
		return false;
	}
}
