using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour {

	private GlobalScore _globalScore;
	public List<MapStation> mapStations;
	public GameObject villager;
	public Material originalMat;
	public Material highlightMat;

	// Use this for initialization
	void Start () {
		_globalScore = GetComponent<GlobalScore>();
		StartCoroutine(Setup());
	}

	// Update is called once per frame
	void Update () {
		int layerMask = 1 << 12;

		RaycastHit hit;

		if (Physics.Raycast(villager.transform.position, villager.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) {
				var station = GetStation(hit.transform);
				if (station != null) {
					station.SetMaterial(highlightMat);
				}
		} else {
			foreach (var station in mapStations) {
				station.SetMaterial(originalMat);
			}
		}
	}

	private void UpdateStationsState() {
		foreach (var station in mapStations) {
			int score = _globalScore.GetScore(station.name);
			station.SetCompletionLevel(score);
		}
	}

	public static void SwitchScene(string name) {
		SceneManager.LoadScene(name);
	}

	private MapStation GetStation(Transform transform) {
		foreach (var station in mapStations) {
			if (station.castleUnlocked.transform == transform) {
					return station;
			}
		}
		return null;
	}

	private IEnumerator Setup() {
		// Wait for _globalScore to be ready (data are fetched)
		yield return new WaitUntil(() => _globalScore.ready);
		UpdateStationsState();
	}
}
