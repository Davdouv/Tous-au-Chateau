﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEvent_MapSelector_CreditEvent : SpeechEvent {
	private bool _hasOpenedAlready = false;

	public override bool MustOpen() {
				// Open automatically
				if (!_hasOpenedAlready) {
					_hasOpenedAlready = true;
					return true;
				}
				return false;
	}

	public override bool MustClose() {
        return false;
	}
}
