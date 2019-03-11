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
    private bool _isCameraDefault = false;

    public bool FindCamera() {
  		if (GameObject.Find("Neck/Camera")) { // VR
  			Debug.Log("VR Camera");
        _isCameraDefault = false;
  			_camera = GameObject.Find("Neck/Camera").GetComponent<Camera>();
        return true;
  		} else if (GameObject.Find("[VRSimulator_CameraRig]")) { // Simulator
  			Debug.Log("Simulator Camera");
        _isCameraDefault = false;
  			_camera = GameObject.Find("Camera (eye)").GetComponent<Camera>();
        return true;
  		} else if (GameObject.Find("Main Camera")) { // Default
  			Debug.Log("Default Camera");
        _isCameraDefault = true;
  			_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        return true;
  		}
      return false;
  	}

    public Camera GetCamera() {
      FindCamera();
      return _camera;
    }

    public bool IsCameraDefault() {
      return _isCameraDefault;
    }
}
