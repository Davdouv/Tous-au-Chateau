using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour {
    public float distance;
    public float angle;

    // a voir
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        angle = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (angle != 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime); // move forward
        }

        //Raycasting
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            angle = Vector3.SignedAngle(transform.forward, hit.normal, Vector3.up);
            angle = angle > 0 ? angle - 90 : angle + 90;
            transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y + angle, 0);
        }
    }
}
