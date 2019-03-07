using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VolcanoSmoke : MonoBehaviour {

    public static Vector3 spawn;
    public static float moveSpeed = 0.001f;
    public static float maxSpeed = 1.0f;

    public static int brainSize = 10;
    public Vector3[] brain = new Vector3[brainSize];   //brain stores vectors of movement
    public int i = 0;   //brain iterator
    public int lifespan =  1;    //number of steps it can take before it dies (increased each 5 generations by population.cs script)

    public bool reachedTheGoal = false;
    public bool dead = false;

    public Material alive;
    public Material notAlive;
    public Material champion;
    public Shader alwaysOnTop;  //this shader makes champion visible over other players

    public float fitness = 0;

    private float distToGround;
    public float distToGoalFromSpawn;

    // Use this for initialization
    void Start (){
        GenerateVectors();
        //spawn = transform.position;
        //distToGround = 117f;
        distToGround = 19f;
    }


    // Unity method for physics update
    void FixedUpdate(){

        if(!dead)
        {
            if (!IsGrounded()) Die();  //they die when there's no ground under them (we are in the air so not really a ground, but compared to initial height position)

            if (i >= brainSize || i >= lifespan) Die();
        }

    }


    public void MovePlayer()
    {
        if (!dead)
        {
            GetComponent<Rigidbody>().AddForce(brain[i] * moveSpeed * 0.01f);
            i++;
 
        }
    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Goal")
        {
            reachedTheGoal = true;
            Die();
        }
    }


    public void Respawn()
    {
        transform.position = spawn;
        GetComponent<Rigidbody>().velocity = Vector3.zero; //stop movement in case it was falling while being dead or sth idk
        i = 0;
        dead = false;
        reachedTheGoal = false;
        GetComponent<Renderer>().material = alive;
    }


    public void Die()
    {
        dead = true;
        GetComponent<Renderer>().material = notAlive;
        GetComponent<Rigidbody>().velocity = Vector3.zero; //stop movement
    }


    public void SetAsChampion()
    {
        GetComponent<Renderer>().material = champion;
        GetComponent<Renderer>().material.shader = alwaysOnTop;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 15.0f);
    }


     private void GenerateVectors()
    {
       for (int j = 0; j < brainSize; j++)
            brain[j] = new Vector3(Random.Range(-10, 11), 0, Random.Range(10, -11));
    }

}
