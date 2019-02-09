using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushable : MonoBehaviour {

    public string _name;
    [Range(0, 500)]
    public int woodGain;
    [Range(0, 500)]
    public int stoneGain;
    [Range(0, 500)]
    public int foodGain;
    [Range(0, 500)]
    public int workForceGain;
    [Range(0, 500)]
    public int motivationGain;

    protected ResourcesPack _gain;

    // Use this for initialization
    void Start () {
        _gain = new ResourcesPack { stone = stoneGain, wood = woodGain, food = foodGain, workForce = workForceGain, motivation = motivationGain };
    }
	
	// Update is called once per frame
    public ResourcesPack Gain()
    {
        return _gain;
    }

	public void Crush()
    {
        Destroy(gameObject);
    }
}
