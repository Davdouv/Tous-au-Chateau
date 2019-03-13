﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - EATEN AGAIN

public class SpeechEvent_MapTuto2_Event2 : SpeechEvent {

    public AICharacter wolf;

    public override bool MustOpen()
    {
        // Open after previous event is done
        if (previousEvent != null && previousEvent.IsDone())
        {
            // Check if all group is dead
            if (currentVillagersGroup.GetNumberOfVillagersAlive() == 0)
            {
                // Check if at least one has been killed by a wolf
                if (currentVillagersGroup.IsDeathCausedBy(DeathReason.WOLF))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override bool MustClose()
    {
        // Crush the wolf
        if (!wolf.GetStats().IsAlive())
        {
            return true;
        }
        return false;
    }
}