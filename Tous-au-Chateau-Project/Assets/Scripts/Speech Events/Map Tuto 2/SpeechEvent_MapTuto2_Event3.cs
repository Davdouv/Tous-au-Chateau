using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3 - CRUSHING A ROCK

public class SpeechEvent_MapTuto2_Event3 : SpeechEvent {

    public override bool MustOpen()
    {
        // Open after previous event is done
        if (previousEvent != null && previousEvent.IsDone())
        {
            // Check if all group is dead
            if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            {
                // Check if at least one has been killed by the void (or the river after updating the map)
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.VOID) || currentVillagersGroup.IsDeathCausedBy(DeathReason.RIVER))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override bool MustClose()
    {
        // Crushing a rock (any rock)
        if (ResourceManager.Instance.GetStone() > 0)
        {
            return true;
        }
        return false;
    }
}
