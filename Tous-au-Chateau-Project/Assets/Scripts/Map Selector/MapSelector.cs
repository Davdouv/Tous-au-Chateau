using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour {

	private GlobalScore _globalScore;
	public List<MapStation> mapStations;

	public bool debugLoadScene = false;

	// Use this for initialization
	void Start () {
		_globalScore = GetComponent<GlobalScore>();
		StartCoroutine(Setup());
	}

	// Update is called once per frame
	void Update () {
		// DEBUG
		if (debugLoadScene) {
			SceneManager.LoadScene("TestScene");
		}

		foreach (var station in mapStations) {
			if (station.isCrushed && station.GetScore() >= 0) {
				SwitchScene(station.levelName);
			}
		}

	}

	private void UpdateStationsState() {
		foreach (var station in mapStations) {
			int score = _globalScore.GetScore(station.name);
			station.SetCompletionLevel(score);
		}
	}

	private void SwitchScene(string name) {
		SceneManager.LoadScene(name);
	}

	private IEnumerator Setup() {
		// Wait for _globalScore to be ready (data are fetched)
		yield return new WaitUntil(() => _globalScore.ready);
		UpdateStationsState();
	}
}
