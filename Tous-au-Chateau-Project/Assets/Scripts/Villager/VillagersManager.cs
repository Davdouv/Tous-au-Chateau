using UnityEngine;
using System.Collections.Generic;

public class VillagersManager : MonoBehaviour
{
    #region Singleton
    private static VillagersManager _instance;

    public static VillagersManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_VillagersManager");
                go.AddComponent<VillagersManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    private List<VillagersGroup> _villagersGroup = new List<VillagersGroup>();

    public void AddGroup(VillagersGroup villagersGroup)
    {
        _villagersGroup.Add(villagersGroup);
    }

    public int GetNumberOfVillagersAlive()
    {
        int count = 0;
        _villagersGroup.ForEach(group => count += group.GetNumberOfVillagersAlive());
        return count;
    }
}
