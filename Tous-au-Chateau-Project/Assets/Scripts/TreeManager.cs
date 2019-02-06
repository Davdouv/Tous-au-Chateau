using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour {

    //Element for axiom
    public string _axiom = "FFL";
    string _lastAxiom;
    public string _ruleB = "F";
    public string _ruleE = "F[FFL]";
    //string treeStr = "FFF[FF[FL]FL]FL";

    public GameObject _treePrefab;
    public GameObject _branch;
    public GameObject _leaf;


    //Collection
    List<Vector3> _pointOfBranch;
    Stack<Vector3> _points;
    Stack<Vector3> _startPoints;
    Stack<Vector3> _direction;
    Stack<int> _coeff;

    //public variable
    public int _iteration = 1;
    public float _minAngle = 30;
    public float _maxAngle = 85;
    public float _length = 10;
    public float _widthMax = 0.7f;
    public float _widthMin = 0.5f;
    public float _sizeLeaf = 1;
    public float axeY = -3;


    //Matrix for movement
    Matrix4x4 _rotatePosX;
    Quaternion _rotationPosX;

    Vector3 _lastPoint;


    // Use this for initialization
    void Start () {
        _pointOfBranch = new List<Vector3>();
        _points = new Stack<Vector3>();
        _startPoints = new Stack<Vector3>();
        _lastPoint = new Vector3(0, axeY, 0);
        _direction = new Stack<Vector3>();
        _coeff = new Stack<int>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            _points.Clear();
            _lastPoint = new Vector3(Random.Range(-50, 50), axeY, Random.Range(-50, 50));
            //_lastAxiom = _axiom;
            //AxiomeIterations(); //create axiom
            BuildTree(); 
        }
    }

    public GameObject BuildTree()
    {
        //Axiom
        _lastPoint = new Vector3(0, axeY, 0);
        _lastAxiom = _axiom;
        AxiomeIterations(); //create axiom

        //first element & direction
        _startPoints.Push(_lastPoint);
        _points.Push(_lastPoint);
        _direction.Push(new Vector3(0, 1, 0));
        _coeff.Push(1);

        GameObject newTree = Instantiate(_treePrefab);
        newTree.transform.parent = this.transform;

        for(int i = 0; i < _lastAxiom.Length; ++i)
        {
            switch (_lastAxiom[i])
            {
                case 'F': //Add branch (which has the last direction of the stack "direction")
                    _lastPoint += _length / _coeff.Peek() * _direction.Peek();
                    _points.Push(_lastPoint);
                    break;

                case 'L': //Add leaf at last point
                    GameObject go = Instantiate(_leaf, _lastPoint, transform.rotation);
                    go.transform.parent = newTree.transform;
                    go.transform.localScale = new Vector3(_sizeLeaf * _length / _coeff.Peek(), _sizeLeaf * _length / _coeff.Peek(), _sizeLeaf * _length / _coeff.Peek());
                    break;

                case '[': //Start a parallel item
                    _points.Push(_lastPoint);
                    _startPoints.Push(_lastPoint); //Add the beginning of the subtree

                    //Creation of the random rotation for the new direction
                    float rand1 = Random.Range(_minAngle, _maxAngle);
                    float rand2 = Random.Range(_minAngle, _maxAngle);
                    int sig1 = -2 * Random.Range(0, 2) + 1;
                    int sig2 = -2 * Random.Range(0, 2) + 1;
                    _rotationPosX = Quaternion.Euler(sig1 * rand1, 0, sig2 * rand2);
                    _rotatePosX = Matrix4x4.Rotate(_rotationPosX);

                    _coeff.Push(_coeff.Peek() + 1);
                    _direction.Push(_rotatePosX.MultiplyPoint3x4(_direction.Peek()).normalized);
                    break;

                case ']': //End a parallel item
                    GameObject subTree = Instantiate(_branch);
                    subTree.transform.parent = newTree.transform;
                    _pointOfBranch.Clear();
                    int pointsCount = 0;

                    //take all points until the beginning of the sub tree
                    while(_points.Peek() != _startPoints.Peek())
                    {
                        _pointOfBranch.Add(_points.Pop());
                        ++pointsCount;
                    }

                    _pointOfBranch.Add(_points.Pop());
                    ++pointsCount;

                    //creation of the lineRenderer
                    subTree.GetComponent<LineRenderer>().positionCount = pointsCount;
                    subTree.GetComponent<LineRenderer>().SetPositions(_pointOfBranch.ToArray());
                    subTree.GetComponent<LineRenderer>().startWidth = _widthMin / _coeff.Peek();
                    subTree.GetComponent<LineRenderer>().endWidth = _widthMax / _coeff.Peek();

                    //depop
                    _direction.Pop();
                    _startPoints.Pop();
                    _coeff.Pop();
                    _lastPoint = _points.Peek();
                    break;

                default:
                    break;
            }
        }

        GameObject tree = Instantiate(_branch);
        tree.transform.parent = newTree.transform;
        _pointOfBranch.Clear();
        int numPoint = _points.Count;

        for(int i =0; i < numPoint; ++i)
        {
            _pointOfBranch.Add(_points.Pop());
        }

        //Creation of the main branch
        tree.GetComponent<LineRenderer>().positionCount = numPoint;
        tree.GetComponent<LineRenderer>().SetPositions(_pointOfBranch.ToArray());
        tree.GetComponent<LineRenderer>().startWidth = _widthMin;
        tree.GetComponent<LineRenderer>().endWidth = _widthMax;

        return newTree;
    }




    //Récupérer de Justine Vuillemot
    private void AxiomeIterations()
    {
        for (int i = 0; i < _iteration; i++)
        {
            _lastAxiom = ApplyRuleOnAxiome();
        }
    }

    private string ApplyRuleOnAxiome()
    {
        string prevAxiome = _lastAxiom;
        string nextAxiome = "";

        while (prevAxiome.IndexOf(_ruleB) != -1)
        {
            int index = prevAxiome.IndexOf(_ruleB); // save the index

            //copy the before sttring
            if (index > 0)
            {
                nextAxiome += prevAxiome.Substring(0, index);
            }

            //replace by the rule end
            nextAxiome += _ruleE;

            //keep only what hasn't been treated yet in the string
            prevAxiome = prevAxiome.Substring(index + _ruleB.Length);
        }

        //In case the end of the string doesn't contain the pattern of rule beginning
        if (prevAxiome != "")
        {
            nextAxiome += prevAxiome;
        }

        return nextAxiome;
    }

}
