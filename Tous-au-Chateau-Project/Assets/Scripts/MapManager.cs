using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

    #region Singleton
    private static MapManager _instance;

    // ***** SINGLETON *****/
    public static MapManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_MapManager");
                go.AddComponent<MapManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    public GameObject _TreesPosition;

    #region Tuto1
    bool once = true;
    private GameObject firstTree = null;
    private List<Crushable> otherTrees;
    private bool isFirstTreeDestroyed = false;
    #endregion

    private void Start()
    {
        otherTrees = new List<Crushable>();

        Generate();        
    }

    public void Generate()
    {
        for (int i = 0; i < _TreesPosition.transform.childCount; i++)
        {
            GameObject go = this.GetComponent<TreeManager>().BuildTree();
            go.transform.position = _TreesPosition.transform.GetChild(i).gameObject.transform.position;

            if (once && GameManager.Instance.tuto)
            {
                once = false;
                go.AddComponent<FirstTree>();
                firstTree = go;
            }
            else if (!once)
            {
                Crushable tree = go.GetComponent<Crushable>();
                tree.canBeCrushed = false;
                otherTrees.Add(tree);
            }
        }
    }

    public bool IsFirstTreeDestroyed()
    {
        return isFirstTreeDestroyed;
    }

    // Called when FirstTree is destroyed
    public void SetFirstTreeDestroyed()
    {
        isFirstTreeDestroyed = true;
        otherTrees.ForEach(tree => tree.canBeCrushed = true);
    }
}
