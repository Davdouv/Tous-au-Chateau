﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1 - INTRODUCTION

public class SpeechEvent_MapTuto1_Event1 : SpeechEvent {
	private bool _hasOpenedAlready = false;
    public bool hasCrushedGround = false;

	public override bool MustOpen() {
        currentVillagersGroup.SetVillagersCanMove(false);

				// Open automatically
				if (!_hasOpenedAlready) {
					_hasOpenedAlready = true;
					return true;
				}
				return false;
	}

	public override bool MustClose() {
        // When crushing the ground near the People.
        return hasCrushedGround;
	}
}
