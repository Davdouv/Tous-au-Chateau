using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {

    public float speed = 1.0f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime) ;
    }
}
