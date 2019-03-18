using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MapPhysicObject {

	public string name;
	public CompletionLevel completionLevel = CompletionLevel.Locked;

	public GameObject castleUnlocked;
	public GameObject castleLocked;
	public GameObject platform;

	public GameObject flag1;
	public GameObject flag2;
	public GameObject flag3;

	public GameObject darkFlag1;
	public GameObject darkFlag2;
	public GameObject darkFlag3;

	public enum CompletionLevel {Locked = -1, Unlocked = 0, OneStar = 1, TwoStars = 2, ThreeStars = 3};

	public bool isCrushed = false;

	// Use this for initialization
	void Start () {
		UpdateDisplay();
	}

	public void SetCompletionLevel(int level) {
		completionLevel = (CompletionLevel) level;
		UpdateDisplay();
	}

	private void UpdateDisplay() {
		switch (completionLevel) {
			case CompletionLevel.Locked:
				LockedDisplay();
				break;
			case CompletionLevel.Unlocked:
				UnlockedDisplay();
				break;
			case CompletionLevel.OneStar:
				OneStarDisplay();
				break;
			case CompletionLevel.TwoStars:
				TwoStarsDisplay();
				break;
			case CompletionLevel.ThreeStars:
				ThreeStarsDisplay();
				break;
			default:
				break;
		}
	}

    private void EnableObj(GameObject gameObject, bool enable = true)
    {
        if (gameObject != null)
            gameObject.SetActive(enable);
    }

	private void LockedDisplay() {
		castleLocked.SetActive(true);
		castleUnlocked.SetActive(false);

        EnableObj(darkFlag1, false);
        EnableObj(darkFlag2, false);
        EnableObj(darkFlag3, false);
        EnableObj(flag1, false);
        EnableObj(flag2, false);
        EnableObj(flag3, false);


        Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.grey);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.grey);
	}

	private void UnlockedDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
        EnableObj(darkFlag1, true);
        EnableObj(darkFlag2, true);
        EnableObj(darkFlag3, true);
        EnableObj(flag1, false);
        EnableObj(flag2, false);
        EnableObj(flag3, false);

        Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.yellow);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.yellow);
	}

	private void OneStarDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
        EnableObj(darkFlag1, false);
        EnableObj(darkFlag2, true);
        EnableObj(darkFlag3, true);
        EnableObj(flag1, true);
        EnableObj(flag2, false);
        EnableObj(flag3, false);

        Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	private void TwoStarsDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);

        EnableObj(darkFlag1, false);
        EnableObj(darkFlag2, false);
        EnableObj(darkFlag3, true);
        EnableObj(flag1, true);
        EnableObj(flag2, true);
        EnableObj(flag3, false);

        Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	private void ThreeStarsDisplay() {
		castleUnlocked.SetActive(true);
        castleLocked.SetActive(false);

        EnableObj(darkFlag1, false);
        EnableObj(darkFlag2, false);
        EnableObj(darkFlag3, false);
        EnableObj(flag1, true);
        EnableObj(flag2, true);
        EnableObj(flag3, true);

        Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	public void SetMaterial(Material mat) {
		var rend = castleUnlocked.GetComponent<Renderer>();
		if (mat != rend.material) {
			rend.material = mat;
		}
	}

	// Update is called once per frame
	void Update () {
		// DEBUG
		UpdateDisplay();
	}

}
