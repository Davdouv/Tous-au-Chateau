using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Population : MonoBehaviour{

    public GameObject volcanoSmoke;
    public GameObject goal;
    private GameObject champion;    //VolcanoSmoke with best fitness score

    public static int volcanoSmokeNum;    //number of volcanoSmoke
    public GameObject[] VolcanoSmokes;
    public GameObject VolcanoThroat;

    //spawn position
    //private Vector3 spawn1 = new Vector3(38f, 19f, -3f);
    //private Vector3 spawn1; 

    private float fitnessSum;
    private float mutationRate = 0.02f; //0.02 means only 2% of movement vectors will be modified for each new VolcanoSmoke
    private int minStep = VolcanoSmoke.brainSize;  //minimum of steps taken to reach the goal
    public int generation = 0;
    private bool noWinnerBefore = true;
    private long k = 0; //update counter


    void Awake()
    {
        VolcanoSmoke.spawn = VolcanoThroat.transform.position;
    }
    // Use this for initialization
    void Start(){
        //volcanoSmokeNum = Menu.volcanoSmokeNum; //number of VolcanoSmokes is taken from other script because it enables changing this value in game by user
        volcanoSmokeNum = 15;
        VolcanoSmokes = new GameObject[volcanoSmokeNum];
        SpawnvolcanoSmoke();
        //spawn1 = VolcanoThroat.transform.position;
    }

    // Unity method for physics update
    void FixedUpdate(){

        if(k % 5 == 0)  //update only once per 5 physics updates
        {
            k = 0;
            if (ReachedTheGoal())
            {
                if(noWinnerBefore)
                {
                    print("Success!!! The Winner was born in " + generation + " generation!");
                    noWinnerBefore = false;
                }
            }      

            if(AllDead())   //if everyone is dead their score is evaluated, the champion is colored green and game pauses for 1 second
            {
                NaturalSelection();
                champion.GetComponent<VolcanoSmoke>().SetAsChampion();                   
                StartCoroutine(PauseAndRespawn()); // respawning needs to be embedded inside coroutine for pause purpose                  
            }
            else
            {
                for(int i = 0; i < volcanoSmokeNum; i++)
                {
                    if (VolcanoSmokes[i].GetComponent<VolcanoSmoke>().i > minStep)
                    {
                        VolcanoSmokes[i].GetComponent<VolcanoSmoke>().Die(); //volcanoSmoke is killed if it has already taken more steps then the best volcanoSmoke that reached the goal (that way they stil learn to optimise their moves to reach the goal faster)
                    }
                    else if (VolcanoSmokes[i].GetComponent<Rigidbody>().velocity.magnitude < VolcanoSmoke.maxSpeed)  //movement vector is applied only if volcanoSmoke hasn't crossed max speed limit 
                    {
                        VolcanoSmokes[i].GetComponent<VolcanoSmoke>().MovePlayer();
                    }

                }
            }

        }
        k++;

    }



    IEnumerator PauseAndRespawn()
    {
        enabled = false;    //turn off update function
        yield return new WaitForSeconds(1.0f);  //pause
        enabled = true;     //turn on update function

        RespawnAll();
        generation++;

        if (generation % 5 == 0) IncreaseLifespan();   //every 5 generations expand their lifetime
    }


    bool AllDead()
    {
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            if (VolcanoSmokes[i].GetComponent<VolcanoSmoke>().dead == false) return false;
        }
        return true;
    }


    bool ReachedTheGoal()   //returns true if any volcanoSmoke reached the goal
    {
        bool rechedTheGoal = false;
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            if (VolcanoSmokes[i].GetComponent<VolcanoSmoke>().reachedTheGoal == true)
            {
                if (VolcanoSmokes[i].GetComponent<VolcanoSmoke>().i < minStep)
                {
                    minStep = VolcanoSmokes[i].GetComponent<VolcanoSmoke>().i;
                }            
                rechedTheGoal = true;
            }
        }

        if(rechedTheGoal) return true;
        else return false;
    }


    void RespawnAll()
    {
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            VolcanoSmokes[i].GetComponent<VolcanoSmoke>().Respawn();
        }

        VolcanoSmokes[0].GetComponent<VolcanoSmoke>().SetAsChampion();    //makes the best volcanoSmoke from previous generation to be green
    }


    void SetChampion()
    {
        float best_score = Vector3.Distance(VolcanoSmokes[0].transform.position, goal.transform.position);
        champion = VolcanoSmokes[0];

        for (int i = 1; i < volcanoSmokeNum; i++)
        {
            float DistanceToGoal = Vector3.Distance(VolcanoSmokes[i].transform.position, goal.transform.position);
            if (DistanceToGoal < best_score)
            {
                best_score = DistanceToGoal;
                champion = VolcanoSmokes[i];
            }
        }
    }


    void IncreaseLifespan()
    {
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            VolcanoSmokes[i].GetComponent<VolcanoSmoke>().lifespan += 5;
        }
    }


    void SpawnvolcanoSmoke()
    {
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            //Spawn volcanoSmoke
            GameObject volcanoSmoke_x;
            volcanoSmoke_x = Instantiate(volcanoSmoke) as GameObject;
            string[] name_tmp = { "VolcanoSmoke", (i + 1).ToString() };
            name = string.Join("", name_tmp, 0, 2);
            volcanoSmoke_x.name = name;

            //Assign volcanoSmoke to array
            VolcanoSmokes[i] = volcanoSmoke_x;

            //Calculate distance from spawn to goal
            VolcanoSmokes[i].GetComponent<VolcanoSmoke>().distToGoalFromSpawn = Vector3.Distance(VolcanoSmokes[i].transform.position, goal.transform.position);

        }
    }


    void NaturalSelection()
    {
        SetChampion();

        CalculateFitness();
        CalculateFitnessSum();

        CopyBrain(VolcanoSmokes[0], champion);    //Champion is always reborn in next generation unchanged

        for (int i = 1; i < volcanoSmokeNum; i++)
        {
            GameObject parent = SelectParent();
            CopyBrain(VolcanoSmokes[i], parent);
            Mutate(VolcanoSmokes[i]);
        }
        
    }


    void CopyBrain(GameObject P1, GameObject P2)
    {
        for( int i = 0; i < VolcanoSmoke.brainSize; i++)
        {
            P1.GetComponent<VolcanoSmoke>().brain[i][0] = P2.GetComponent<VolcanoSmoke>().brain[i][0];
            P1.GetComponent<VolcanoSmoke>().brain[i][1] = P2.GetComponent<VolcanoSmoke>().brain[i][1];
            P1.GetComponent<VolcanoSmoke>().brain[i][2] = P2.GetComponent<VolcanoSmoke>().brain[i][2];
        }
    }



    void CalculateFitness()
    {
        for (int i = 0; i < volcanoSmokeNum; i++)
        {
            float DistanceToGoal = Vector3.Distance(VolcanoSmokes[i].transform.position, goal.transform.position);

            if(VolcanoSmokes[i].GetComponent<VolcanoSmoke>().reachedTheGoal)
            {
                int step = VolcanoSmokes[i].GetComponent<VolcanoSmoke>().i;
                float distToGoalFromSpawn = VolcanoSmokes[i].GetComponent<VolcanoSmoke>().distToGoalFromSpawn;
                VolcanoSmokes[i].GetComponent<VolcanoSmoke>().fitness = 1.0f / 24 + distToGoalFromSpawn * 100 / (step * step);
            }
            else
            {
                VolcanoSmokes[i].GetComponent<VolcanoSmoke>().fitness = 10.0f / (DistanceToGoal * DistanceToGoal * DistanceToGoal * DistanceToGoal);
            }
        }
    }


    void CalculateFitnessSum()
    {
        fitnessSum = 0;
        for (int i = 0; i < volcanoSmokeNum; i++)
            fitnessSum += VolcanoSmokes[i].GetComponent<VolcanoSmoke>().fitness;
    }


    GameObject SelectParent()
    {
        float rand = Random.Range(0.0f, fitnessSum);
        float runningSum = 0;

        for(int i = 0; i < volcanoSmokeNum; i++)
        {
            runningSum += VolcanoSmokes[i].GetComponent<VolcanoSmoke>().fitness;
            if (runningSum >= rand)
            {
                return VolcanoSmokes[i];
            }
        }

        return null; //should never come to this
    }


    void Mutate(GameObject VolcanoSmoke)
    {    
        for (int i = 0; i < VolcanoSmoke.GetComponent<VolcanoSmoke>().lifespan; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            if (rand < mutationRate)
            {
                //VolcanoSmoke.GetComponent<VolcanoSmoke>().brain[i] = new Vector3(Random.Range(10, -11), 0, Random.Range(10, -11));
                VolcanoSmoke.GetComponent<VolcanoSmoke>().brain[i] = new Vector3(0, Random.Range(-30, 33), Random.Range(30, -33));

            }
        }
    }

}
