using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - BUILDING A DIRECTIONAL PANEL

public class SpeechEvent_MapTuto3_Event3 : SpeechEvent {

    public bool hasDirectionalPanelLanded = false;

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
        // Opening the Construction Panel and placing the Directional Panel
        // (not necessary at the right place, could be anywhere)
        return hasDirectionalPanelLanded;
    }
}
