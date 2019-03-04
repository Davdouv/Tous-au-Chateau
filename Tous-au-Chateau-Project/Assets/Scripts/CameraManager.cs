using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    #region Singleton
    private static CameraManager _instance;

    public static CameraManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_CameraManager");
                go.AddComponent<CameraManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    private Camera _camera;

    private void Start()
    {

    }

    private void Update()
    {

    }
}
