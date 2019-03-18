using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        float xRot = transform.rotation.x;
        transform.LookAt(CameraManager.Instance.GetCamera().transform);
    }
}
