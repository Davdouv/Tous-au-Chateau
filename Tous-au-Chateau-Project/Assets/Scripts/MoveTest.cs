using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {

    float speed = 10f;
	
	// Update is called once per frame
	void Update ()
    {
        // Basic 2D movements for test
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0));
        transform.Translate(new Vector3(0, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0));
    }
}
