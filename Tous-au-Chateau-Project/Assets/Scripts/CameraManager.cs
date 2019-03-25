using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CameraShaker))]
public class CameraManager : MonoBehaviour
{

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

    private GameObject _camera;
    private bool _isCameraDefault = false;

    private GameObject _cameraDefault;

    private CameraShaker cameraShaker;

    private void Start()
    {
        cameraShaker = GetComponent<CameraShaker>();
        _cameraDefault = GameObject.Find("Main Camera");
    }

    public bool FindCamera()
    {
        if (GameObject.Find("Camera (eye)"))
        { // VR
          Debug.Log("VR Camera (eye)");
            _isCameraDefault = false;
            _camera = GameObject.Find("Camera (eye)");
            return true;
        }
        else if (GameObject.Find("Neck"))
        { // Simulator
          Debug.Log("Simulator Camera (neck)");
            _isCameraDefault = false;
            _camera = GameObject.Find("Neck");
            return true;
        }
        else if (GameObject.Find("[VRSimulator_CameraRig]"))
        { // Simulator
          Debug.Log("Simulator Camera");
            _isCameraDefault = false;
            _camera = GameObject.Find("[VRSimulator_CameraRig]");
            return true;
        }
        else if (GameObject.Find("Main Camera"))
        { // Default
          Debug.Log("Default Camera");
            _isCameraDefault = true;
            _camera = GameObject.Find("Main Camera");
            return true;
        }
        return false;
    }

    public Camera GetCamera()
    {
        FindCamera();
        if (_camera.GetComponent<Camera>() != null) {
          return _camera.GetComponent<Camera>();
        }
        return null;
    }

    public Transform GetCameraTransform()
    {
        FindCamera();
        return _camera.transform;
    }

    public bool IsCameraDefault(Camera camera)
    {
        return _cameraDefault.GetComponent<Camera>() == camera;
    }

    public bool IsCameraDefault(Transform transform)
    {
        return _cameraDefault.transform == transform;
    }

    public void ShakeCamera()
    {
        if (cameraShaker)
        {
            if (GetCamera())
            {
                cameraShaker.ShakeCamera(GetCamera().transform.parent.transform);
            }
            else
            {
                cameraShaker.ShakeCamera(_cameraDefault.transform);
            }
        }
    }
}
