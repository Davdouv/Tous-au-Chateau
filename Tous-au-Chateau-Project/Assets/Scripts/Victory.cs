using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victory : TriggerZone {

    private int scoreCount = 0;
    public Text score;

	// Use this for initialization
	void Start () {
    }

    public override void CollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Villager>().SetHasReachedObjectif();
        collision.transform.position = this.transform.position;

        UpdateScore();

        if (VillagersManager.Instance.HasLastVillagersReachedObjectif())
        {
            Debug.Log("VICTORY");
            GameManager.Instance.GameWon(scoreCount);
        }
    }

    private void UpdateScore()
    {
        ++scoreCount;
        score.text = scoreCount.ToString();
    }
}
