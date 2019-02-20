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
		if (debugLoadScene) {
			SceneManager.LoadScene("TestScene");
		}
	}

	private void UpdateStationsState() {
		foreach (var station in mapStations) {
			int score = _globalScore.GetScore(station.name);
			station.SetCompletionLevel(score);
		}
	}

	private IEnumerator Setup() {
		// Wait for _globalScore to be ready (data are fetched)
		yield return new WaitUntil(() => _globalScore.ready);
		UpdateStationsState();
	}
}
