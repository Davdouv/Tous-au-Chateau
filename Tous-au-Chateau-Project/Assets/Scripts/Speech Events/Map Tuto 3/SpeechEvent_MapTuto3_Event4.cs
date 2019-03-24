using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 4 - BUILDING ANOTHER DIRECTIONAL PANEL

public class SpeechEvent_MapTuto3_Event4 : SpeechEvent {

    public bool hasDirectionalPanelLanded = false;

    public override bool MustOpen()
    {
        // Open after previous event is done
        if (previousEvent != null && previousEvent.IsDone())
        {
            // Check if all group is dead
            if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            {
                // Check if at least one has been killed by the void
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.VOID))
                {
                    return true;
                }
            }
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
