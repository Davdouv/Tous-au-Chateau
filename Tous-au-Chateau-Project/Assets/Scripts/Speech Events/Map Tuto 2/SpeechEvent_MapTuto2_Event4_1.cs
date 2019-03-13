using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4 - BUILDING A WALL (optionnal message)

public class SpeechEvent_MapTuto2_Event4_1 : SpeechEvent {

    public bool hasStoneWallLanded = false;

    public override bool MustOpen()
    {
        // Open after previous event is done
        if (previousEvent != null && previousEvent.IsDone())
        {
            return true;
        }
        return false;
    }

    public override bool MustClose()
    {
        // Opening the Construction Panel and placing the wood board
        // (not necessary at the right place, could be anywhere)
        return hasStoneWallLanded;
    }
}
