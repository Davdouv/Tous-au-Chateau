using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour {

    //Element for axiom
    public string axiom = "FL";
    string lastAxiom;
    public string ruleB = "F";
    public string ruleE = "F[FL]F";
    //string treeStr = "FFF[FF[FL]FL]FL";

    public GameObject treePrefab;
    public GameObject branch;
    public GameObject leaf;


    //Collection
    List<Vector3> pointOfBranch;
    Stack<Vector3> points;
    Stack<Vector3> startPoints;
    Stack<Vector3> direction;
    Stack<int> coeff;

    //public variable
    public int iteration = 1;
    public float minAngle = 30;
    public float maxAngle = 85;
    public float length = 10;
    public float widthMax = 0.7f;
    public float widthMin = 0.5f;
    public float sizeLeaf = 1;


    //Matrix for movement
    Matrix4x4 rotatePosX;
    Quaternion rotationPosX;

    Vector3 lastPoint;


    // Use this for initialization
    void Start () {
        pointOfBranch = new List<Vector3>();
        points = new Stack<Vector3>();
        startPoints = new Stack<Vector3>();
        lastPoint = new Vector3(0, 0, 0);
        direction = new Stack<Vector3>();
        coeff = new Stack<int>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            points.Clear();
            lastPoint = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            lastAxiom = axiom;
            AxiomeIterations(); //create axiom
            buildTree(); 
        }
    }

    void buildTree()
    {
        //first element & direction
        startPoints.Push(lastPoint);
        points.Push(lastPoint);
        direction.Push(new Vector3(0, 1, 0));
        coeff.Push(1);

        GameObject newTree = Instantiate(treePrefab);

        for(int i = 0; i < lastAxiom.Length; ++i)
        {
            //Debug.Log(lastPoint);
            switch (lastAxiom[i])
            {
                case 'F': //Add branch (which has the last direction of the stack "direction")
                    lastPoint += length / coeff.Peek() * direction.Peek();
                    points.Push(lastPoint);
                    break;

                case 'L': //Add leaf at last point
                    GameObject go = Instantiate(leaf, lastPoint, transform.rotation);
                    go.transform.localScale = new Vector3(sizeLeaf * length / coeff.Peek(), sizeLeaf * length / coeff.Peek(), sizeLeaf * length / coeff.Peek());
                    break;

                case '[': //Start a parallel item
                    points.Push(lastPoint);
                    startPoints.Push(lastPoint); //Add the beginning of the subtree

                    //Creation of the random rotation for the new direction
                    float rand1 = Random.Range(minAngle, maxAngle);
                    float rand2 = Random.Range(minAngle, maxAngle);
                    int sig1 = -2 * Random.Range(0, 2) + 1;
                    int sig2 = -2 * Random.Range(0, 2) + 1;
                    rotationPosX = Quaternion.Euler(sig1 * rand1, 0, sig2 * rand2);
                    rotatePosX = Matrix4x4.Rotate(rotationPosX);

                    coeff.Push(coeff.Peek() + 1);
                    direction.Push(rotatePosX.MultiplyPoint3x4(direction.Peek()).normalized);
                    break;

                case ']': //End a parallel item
                    GameObject subTree = Instantiate(branch);
                    subTree.transform.parent = newTree.transform;
                    pointOfBranch.Clear();
                    int pointsCount = 0;

                    //take all points until the beginning of the sub tree
                    while(points.Peek() != startPoints.Peek())
                    {
                        pointOfBranch.Add(points.Pop());
                        ++pointsCount;
                    }

                    pointOfBranch.Add(points.Pop());
                    ++pointsCount;

                    //creation of the lineRenderer
                    subTree.GetComponent<LineRenderer>().positionCount = pointsCount;
                    subTree.GetComponent<LineRenderer>().SetPositions(pointOfBranch.ToArray());
                    subTree.GetComponent<LineRenderer>().startWidth = widthMin / coeff.Peek();
                    subTree.GetComponent<LineRenderer>().endWidth = widthMax / coeff.Peek();

                    //depop
                    direction.Pop();
                    startPoints.Pop();
                    coeff.Pop();
                    lastPoint = points.Peek();
                    break;

                default:
                    break;
            }
        }

        GameObject tree = Instantiate(branch);
        tree.transform.parent = newTree.transform;
        pointOfBranch.Clear();
        int numPoint = points.Count;

        for(int i =0; i < numPoint; ++i)
        {
            Debug.Log(points.Peek());
            pointOfBranch.Add(points.Pop());
        }

        //Creation of the main branch
        tree.GetComponent<LineRenderer>().positionCount = numPoint;
        tree.GetComponent<LineRenderer>().SetPositions(pointOfBranch.ToArray());
        tree.GetComponent<LineRenderer>().startWidth = widthMin;
        tree.GetComponent<LineRenderer>().endWidth = widthMax;
    }




    //Récupérer de Justine Vuillemot
    private void AxiomeIterations()
    {
        Debug.Log("Axiome : " + lastAxiom);

        for (int i = 0; i < iteration; i++)
        {
            lastAxiom = ApplyRuleOnAxiome();
        }

        Debug.Log("Created tree : " + lastAxiom);
    }

    private string ApplyRuleOnAxiome()
    {
        string prevAxiome = lastAxiom;
        string nextAxiome = "";

        while (prevAxiome.IndexOf(ruleB) != -1)
        {
            int index = prevAxiome.IndexOf(ruleB); // save the index

            //copy the before sttring
            if (index > 0)
            {
                nextAxiome += prevAxiome.Substring(0, index);
            }

            //replace by the rule end
            nextAxiome += ruleE;

            //keep only what hasn't been treated yet in the string
            prevAxiome = prevAxiome.Substring(index + ruleB.Length);
        }

        //In case the end of the string doesn't contain the pattern of rule beginning
        if (prevAxiome != "")
        {
            nextAxiome += prevAxiome;
        }

        return nextAxiome;
    }

}
