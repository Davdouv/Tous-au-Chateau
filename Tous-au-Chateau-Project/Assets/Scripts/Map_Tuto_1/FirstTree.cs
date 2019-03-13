using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTree : MonoBehaviour
{
    // Callback
    private void OnDestroy()
    {
        MapManager.Instance.SetFirstTreeDestroyed();
    }
}
