using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour {

    public List<GameObject> rocks;
    public string genomeString = "110011001001011010010110011000010010110011111001101101101111110101010";
    public float speed = 2;
    public float scale = 0.1f;

    private List<float> oscil;
    private List<GameObject> element;
    private List<int> genome;

    private Quaternion basicRotationLeft;
    private Quaternion basicRotationRight;

    private bool attack;
    private bool attackUp;
    private bool attackDown;

    private float time;


    //110011001001011010010110011000010010110011111001101101101111110101010

    // Use this for initialization
    void Start () {
        element = new List<GameObject>();
        genome = new List<int>();
        oscil = new List<float>();
        TranslateGenome();
        DrawGolem();
        for(int i = 0; i < 10; ++i)
        {
            oscil.Add(Random.Range(0.005f, 0.01f));
        }

        attack = false;
   
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < element.Count; ++i)
        {
            element[i].transform.position += new Vector3(0, oscil[i]*Mathf.Sin(speed*(Time.time+0.5f*i)), 0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            Attack();

        }

        if (attack == true)
        {
            
            time += Time.deltaTime;
            element[2].transform.rotation = Quaternion.Lerp(element[3].transform.rotation, Quaternion.AngleAxis(-150, new Vector3(0, 0, 1)), 0.1f);
            element[3].transform.rotation = Quaternion.Lerp(element[3].transform.rotation, Quaternion.AngleAxis(-150, new Vector3(0, 0, 1)), 0.1f);

            if (time > 1.0f)
            {
                attack = false;
                attackDown = true;
                time = 0;
            }
        }

        if (attackDown == true)
        {
            
            time += Time.deltaTime;
            element[2].transform.rotation = Quaternion.Lerp(element[3].transform.rotation, basicRotationLeft, 0.1f);
            element[3].transform.rotation = Quaternion.Lerp(element[3].transform.rotation, basicRotationRight, 0.1f);

            if (time > 1.0f)
            {
                attackDown = false;
            }
        }
    }

    public void Attack()
    {
        basicRotationLeft = element[2].transform.rotation;
        basicRotationRight = element[3].transform.rotation;
        time = 0;
        attack = true;
    }

    void TranslateGenome()
    {
        for(int i = 0; i < genomeString.Length; ++i)
        {
            if(genomeString[i] == '0')
            {
                genome.Add(0);
            }
            else
            {
                genome.Add(1);
            }
            
        }
    }

    void getAllElement()
    {
        //element.Add(gameObject.transform.GetChild(0).gameObject);
    }


    //CREATE A GOLEM
    void DrawGolem()
    {
        //INITIALIZE VALUE FROM GENOTYPE
        //Shoulder
        float shoulderPos;
        float sizeShoulder;
        float sizeLegDistance;
        //int shoulderFormTwo;
        //float shoulderHeightTwo;
        //Body
        int bodyForm;
        float bodyHeight;
        //Head
        int headForm;
        float headHeight;
        //FrontArm
        int frontArmFormOne;
        float frontArmHeightOne;
        int frontArmFormTwo;
        float frontArmHeightTwo;
        //Arm
        int armFormOne;
        float armHeightOne;
        int armFormTwo;
        float armHeightTwo;
        //Leg
        int legFormOne;
        float legHeightOne;
        int legFormTwo;
        float legHeightTwo;
        //Foot
        int footFormOne;
        float footHeightOne;
        int footFormTwo;
        float footHeightTwo;

        
        //Save value
        //Shoulder
        shoulderPos = (genome[0] + 2 * genome[1] + 4 * genome[2]) / 8.0f * (1.5f);
        sizeShoulder = 3.0f + (genome[3] + 2 * genome[4] + 4 * genome[5]) / 8.0f * (6.0f - 3.0f);
        sizeLegDistance = 2.5f + (genome[6] + 2 * genome[7] + 4 * genome[8]) / 8.0f * (5.5f - 2.5f);
        //Body
        Debug.Log((int) genome[11]);
        Debug.Log((int) genome[10]);
        Debug.Log((int) genome[9]);
        Debug.Log(4 * genome[11] + 2 * genome[10] + genome[9]);
        bodyForm = 4 * genome[11] + 2 * genome[10] + genome[9]; //0 à 7
        bodyHeight = 4.0f + (genome[12] + 2 * genome[13] + 4 * genome[14]) / 8.0f * (7.0f - 4.0f);
        //Head
        headForm = 4 * genome[15] + 2 * genome[16] + genome[17]; //0 à 7
        headHeight = 0.5f + (genome[18] + 2 * genome[19] + 4 * genome[20]) / 8.0f * (2.0f - 0.5f);
        //FrontArm
        frontArmFormOne = 4 * genome[23] + 2 * genome[22] + genome[21]; //0 à 7
        frontArmHeightOne = 0.7f + (genome[24] + 2 * genome[25] + 4 * genome[26]) / 8.0f * (6.0f - 0.7f);
        frontArmFormTwo = 4 * genome[29] + 2 * genome[28] + genome[27]; //0 à 7
        frontArmHeightTwo = 0.7f + (genome[30] + 2 * genome[31] + 4 * genome[32]) / 8.0f * (6.0f - 0.7f);
        //Arm
        armFormOne = 4 * genome[35] + 2 * genome[34] + genome[33]; //0 à 7
        armHeightOne = 1.0f + (genome[36] + 2 * genome[37] + 4 * genome[38]) / 8.0f * (3.0f - 1.0f);
        armFormTwo = 4 * genome[41] + 2 * genome[40] + genome[39]; //0 à 7
        armHeightTwo = 1.0f + (genome[42] + 2 * genome[43] + 4 * genome[44]) / 8.0f * (3.0f - 1.0f);
        //Leg
        legFormOne = 4 * genome[47] + 2 * genome[46] + genome[45]; //0 à 7
        legHeightOne = 0.7f + (genome[48] + 2 * genome[49] + 4 * genome[50]) / 8.0f * (4.0f - 0.7f);
        legFormTwo = 4 * genome[53] + 2 * genome[52] + genome[51]; //0 à 7
        legHeightTwo = 0.7f + (genome[54] + 2 * genome[55] + 4 * genome[56]) / 8.0f * (4.0f - 0.7f);
        //Foot
        footFormOne = 4 * genome[59] + 2 * genome[58] + genome[57]; //0 à 7
        footHeightOne = 1.0f + (genome[60] + 2 * genome[61] + 4 * genome[62]) / 8.0f * (3.0f - 1.0f);
        footFormTwo = 4 * genome[65] + 2 * genome[64] + genome[63]; //0 à 7
        footHeightTwo = 1.0f + (genome[66] + 2 * genome[67] + 4 * genome[68]) / 8.0f * (3.0f - 1.0f);


        //INITIALISATION 3D SKELETON
        GameObject newBody = Instantiate(rocks[bodyForm]);
        //GameObject newShoulder = Instantiate(rocks[headForm]);
        GameObject newLeftFrontArm = Instantiate(rocks[frontArmFormOne]);
        GameObject newRightFrontArm = Instantiate(rocks[frontArmFormTwo]);
        GameObject newLeftArm = Instantiate(rocks[armFormOne]);
        GameObject newRightArm = Instantiate(rocks[armFormTwo]);
        //GameObject newLegs = Instantiate(rocks[legFormOne]);
        GameObject newLeftLeg = Instantiate(rocks[legFormOne]);
        GameObject newRightLeg = Instantiate(rocks[legFormTwo]);
        GameObject newLeftFoot = Instantiate(rocks[footFormOne]);
        GameObject newRightFoot = Instantiate(rocks[footFormTwo]);
        GameObject head = Instantiate(rocks[headForm]);

        //Apply elements to bones and change their positions and rotation
        newBody.transform.localScale = new Vector3(3, bodyHeight, 3);
        newBody.transform.localRotation = Quaternion.Euler(180, 0, 0);
        newBody.transform.position = new Vector3(0.0f, 14.0f, 0.0f);

        head.transform.localScale = new Vector3(3, headHeight, 3);
        head.transform.position = new Vector3(0.0f, 12.0f + bodyHeight / 2.0f, 0.0f);

        //newShoulder.transform.localScale = new Vector3(0.1f, 1.5f / 2.0f, 0.1f);
        //newShoulder.transform.localRotation = Quaternion.Euler(90, 0, 0);
        Vector3 shoulderPosistion = new Vector3(0.0f, newBody.transform.position.y - 2.0f + newBody.transform.localScale.y / 2.0f - shoulderPos, 0.0f);

        newLeftFrontArm.transform.localScale = new Vector3(2, frontArmHeightOne, 2);
        newLeftFrontArm.transform.localRotation = Quaternion.Euler(40, 0, 0);
        newLeftFrontArm.transform.position = shoulderPosistion - new Vector3(0.0f, 0.0f, sizeShoulder / 2.0f) + newLeftFrontArm.transform.localScale.y / 2.0f * -1 * newLeftFrontArm.transform.up.normalized;

        newRightFrontArm.transform.localScale = new Vector3(2, frontArmHeightTwo, 2);
        newRightFrontArm.transform.localRotation = Quaternion.Euler(-40, 0, 0);
        newRightFrontArm.transform.position = shoulderPosistion + new Vector3(0.0f, 0.0f, sizeShoulder / 2.0f) + newRightFrontArm.transform.localScale.y / 2.0f * -1 * newRightFrontArm.transform.up.normalized;

        newLeftArm.transform.localScale = new Vector3(2, armHeightOne, 2);
        newLeftArm.transform.position = newLeftFrontArm.transform.position - newLeftFrontArm.transform.localScale.y / 2.0f * newLeftFrontArm.transform.up.normalized + newLeftArm.transform.localScale.y / 2.0f * -1 * newLeftArm.transform.up.normalized;

        newRightArm.transform.localScale = new Vector3(2, armHeightTwo, 2);
        newRightArm.transform.position = newRightFrontArm.transform.position - newRightFrontArm.transform.localScale.y / 2.0f * newRightFrontArm.transform.up.normalized + newRightArm.transform.localScale.y / 2.0f * -1 * newRightArm.transform.up.normalized;


        Vector3 legPosition = new Vector3(0.0f, newBody.transform.position.y - 2.0f - newBody.transform.localScale.y / 2.0f, 0.0f);

        newLeftLeg.transform.localScale = new Vector3(2, legHeightOne, 2);
        newLeftLeg.transform.position = legPosition - new Vector3(0.0f, 0.0f, sizeLegDistance / 2.0f) + newLeftLeg.transform.localScale.y / 2.0f * -1 * newLeftLeg.transform.up.normalized;

        newRightLeg.transform.localScale = new Vector3(2, legHeightTwo, 2);
        newRightLeg.transform.position = legPosition + new Vector3(0.0f, 0.0f, sizeLegDistance / 2.0f) + newRightLeg.transform.localScale.y / 2.0f * -1 * newRightLeg.transform.up.normalized;

        newLeftFoot.transform.localScale = new Vector3(2, footHeightOne, 2);
        newLeftFoot.transform.localRotation = Quaternion.Euler(-16, 0, 0);
        newLeftFoot.transform.position = newLeftLeg.transform.position - newLeftLeg.transform.localScale.y / 2.0f * newLeftLeg.transform.up.normalized + newLeftFoot.transform.localScale.y / 2.0f * -1 * newLeftFoot.transform.up.normalized;

        newRightFoot.transform.localScale = new Vector3(2, footHeightTwo, 2);
        newRightFoot.transform.position = newRightLeg.transform.position - newRightLeg.transform.localScale.y / 2.0f * newRightLeg.transform.up.normalized + newRightFoot.transform.localScale.y / 2.0f * -1 * newRightFoot.transform.up.normalized;

        //ADD SKELETON TO GOLEM
        newBody.transform.parent = gameObject.transform;
        head.transform.parent = newBody.transform;
        //newShoulder.transform.parent = newGolem.transform;
        newLeftFrontArm.transform.parent = newBody.transform;
        newRightFrontArm.transform.parent = newBody.transform;
        newLeftArm.transform.parent = newLeftFrontArm.transform;
        newRightArm.transform.parent = newRightFrontArm.transform;
        //newLegs.transform.parent = newGolem.transform;
        newLeftLeg.transform.parent = newBody.transform;
        newRightLeg.transform.parent = newBody.transform;
        newLeftFoot.transform.parent = newLeftLeg.transform;
        newRightFoot.transform.parent = newRightLeg.transform;

        element.Add(newBody);
        element.Add(head);
        element.Add(newLeftFrontArm);
        element.Add(newRightFrontArm);
        element.Add(newLeftArm);
        element.Add(newRightArm);
        element.Add(newLeftLeg);
        element.Add(newRightLeg);
        element.Add(newLeftFoot);
        element.Add(newRightFoot);
    }
}
