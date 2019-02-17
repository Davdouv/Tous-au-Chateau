using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MonoBehaviour {

	public string name;
	public CompletionLevel completionLevel = CompletionLevel.Locked;

	public GameObject castleUnlocked;
	public GameObject castleLocked;
	public GameObject platform;

	public GameObject flag1;
	public GameObject flag2;
	public GameObject flag3;

	public GameObject darkFlags;

	public enum CompletionLevel {Locked = -1, Unlocked = 0, OneStar = 1, TwoStars = 2, ThreeStars = 3};

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

	private void LockedDisplay() {
		castleLocked.SetActive(true);
		castleUnlocked.SetActive(false);
		darkFlags.SetActive(false);
		flag1.SetActive(false);
		flag2.SetActive(false);
		flag3.SetActive(false);

		Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.grey);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.grey);
	}

	private void UnlockedDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
		darkFlags.SetActive(true);
		flag1.SetActive(false);
		flag2.SetActive(false);
		flag3.SetActive(false);

		Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.yellow);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.yellow);
	}

	private void OneStarDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
		darkFlags.SetActive(false);
		flag1.SetActive(true);
		flag2.SetActive(false);
		flag3.SetActive(false);

		Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	private void TwoStarsDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
		darkFlags.SetActive(false);
		flag1.SetActive(true);
		flag2.SetActive(true);
		flag3.SetActive(false);

		Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	private void ThreeStarsDisplay() {
		castleUnlocked.SetActive(true);
		castleLocked.SetActive(false);
		darkFlags.SetActive(false);
		flag1.SetActive(true);
		flag2.SetActive(true);
		flag3.SetActive(true);

		Renderer platformRend = platform.GetComponent<Renderer>();
		platformRend.material.shader = Shader.Find("_Color");
		platformRend.material.SetColor("_Color", Color.green);
		platformRend.material.shader = Shader.Find("Specular");
		platformRend.material.SetColor("_SpecColor", Color.green);
	}

	// Update is called once per frame
	void Update () {
		// DEBUG
		UpdateDisplay();
	}

}
