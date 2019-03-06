using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

		SaveManager.Save(new LevelScore("Test Castle", 10, 0.2f));
		SceneManager.LoadScene("Map Selector");

	}

	// Update is called once per frame
	void Update () {

	}
}
