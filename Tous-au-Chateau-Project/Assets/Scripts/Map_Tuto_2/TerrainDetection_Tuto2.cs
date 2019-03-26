using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetection_Tuto2 : MonoBehaviour {

    public SpeechEvent_MapTuto2_Event4 speechEvent4;
    public SpeechEvent_MapTuto2_Event4_1 speechEvent4_1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10) // 10 : Building
        {
            collision.gameObject.GetComponent<Building>().SetHasLanded();
            if (collision.gameObject.GetComponent<StoneWall>())
            {
                // Tell the event the stone wall has landed
                speechEvent4.hasStoneWallLanded = true;
                //Debug.Log("Has Landed");
                /*
                if (speechEvent4_1)
                {
                    // If the wall isn't placed at the right position
                    if (!speechEvent4_1.isStoneWallPlacedWell)
                    {
                        Debug.Log("Not Well placed");
                        // Destroy it and give back the resources
                        StartCoroutine(DestroyWall(collision.gameObject));
                    }
                }
                */
            }
        }
    }

    private IEnumerator DestroyWall(GameObject wall)
    {
        yield return new WaitForSeconds(2);
        if (wall)
        {
            wall.GetComponent<Crushable>().Crush();
            ResourceManager.Instance.AddResources(new ResourcesPack(0, 10, 0, 0, 0));
        }
    }
}

