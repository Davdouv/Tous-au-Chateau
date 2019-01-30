using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeHeightMap : MonoBehaviour {

    public GameObject player;
    private Vector3 _playerPosition;

    public void IncreaseX()
    {
        ChangePosition(0.01f, 0, 0);
    }

    public void DecreaseX()
    {
        ChangePosition(-0.01f, 0, 0);
    }

    public void IncreaseY()
    {
        ChangePosition(0, 0.01f, 0);
    }

    public void DecreaseY()
    {
        ChangePosition(0, -0.01f, 0);
    }

    public void IncreaseZ()
    {
        ChangePosition(0, 0, 0.01f);
    }

    public void DecreaseZ()
    {
        ChangePosition(0, 0, -0.01f);
    }

    public Vector3 ChangePosition(float shiftX, float shiftY, float shiftZ)
    {
        return new Vector3(player.transform.position.x + shiftX, player.transform.position.y + shiftY, player.transform.position.z + shiftZ);
    }
}
