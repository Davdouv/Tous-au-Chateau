using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : TriggerZone {

    private uint scoreCount = 0;
    public Text score;

	// Use this for initialization
	void Start () {
    }

    public override void CollisionEnter(Collision collision)
    {
        Debug.Log("YO");
        collision.gameObject.GetComponent<Villager>().SetHasReachedObjectif();
        collision.transform.position = this.transform.position;

        UpdateScore();

        if (VillagersManager.Instance.HasLastVillagersReachedObjectif())
        {
            Debug.Log("VICTORY");
            GameManager.Instance.GameWon();
        }
    }

    private void UpdateScore()
    {
        ++scoreCount;
        /*
        if (scoreCount % 3 == 0)
        {
            scoreCount /= 3;
        }
        */
        score.text = scoreCount.ToString();
    }
}
