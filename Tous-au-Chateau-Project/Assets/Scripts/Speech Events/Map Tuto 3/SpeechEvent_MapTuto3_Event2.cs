using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - FALL INTO THE VOID

public class SpeechEvent_MapTuto3_Event2 : SpeechEvent {

    private bool stopWait = false;

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
        // Crush some trees
        if (ResourceManager.Instance.GetWood() > 0)
        {
            StartCoroutine(WaitSeconds());
            return stopWait;
        }
        return false;
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        stopWait = true;
    }
}
