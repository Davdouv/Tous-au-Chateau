using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1 - INTRODUCTION

public class SpeechEvent_MapA_Event1 : SpeechEvent {
	private bool _hasOpenedAlready = false;

	public override bool MustOpen() {
        currentVillagersGroup.SetVillagersCanMove(false);

		// Open automatically
		if (!_hasOpenedAlready)
        {
			_hasOpenedAlready = true;
			return true;
		}
		return false;
	}

    public override bool MustClose()
    {
        // When any action is done from the player (any key pressed)
        // hasDoneAction = false;
        if (hasDoneAction)
        {
            currentVillagersGroup.SetVillagersCanMove(true);
        }
        return hasDoneAction;
    }
}
