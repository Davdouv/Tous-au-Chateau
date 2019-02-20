using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour {

	private GlobalScore _globalScore;
	public List<MapStation> mapStations;

	// Use this for initialization
	void Start () {
		_globalScore = GetComponent<GlobalScore>();
		UpdateStationsState();
	}

	// Update is called once per frame
	void Update () {

	}

	private void UpdateStationsState() {
		foreach (var station in mapStations) {
			int score = _globalScore.GetScore(station.name);
			station.SetCompletionLevel(score);
		}
	}
}
