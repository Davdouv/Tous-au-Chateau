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

	public bool debug = false;

	// Use this for initialization
	void Start () {
		_globalScore = GetComponent<GlobalScore>();
		StartCoroutine(Setup());
	}

	private void CheckIfTutorialMustStart() {
		if (!debug) {
			var tuto1 = mapStations[0].name;
			if (_globalScore.GetScore(tuto1) <= 0) {
				SwitchScene(tuto1);
			}
			// else if (_globalScore.GetScore(tuto2) <= 0) {
			// 	SwitchScene(tuto2);
			// }
			// else if (_globalScore.GetScore(tuto3) <= 0) {
			// 	SwitchScene(tuto3);
			// }
		}
	}

	// Update is called once per frame
	void Update () {

	}

  public void Highlight(Transform hitTransform)
  {
      var station = GetStation(hitTransform);
      if (station != null)
      {
          station.SetMaterial(highlightMat);
      }
  }

  public void DontHighlight()
  {
      foreach (var station in mapStations)
      {
          station.SetMaterial(originalMat);
      }
  }

	private void UpdateStationsState() {
		foreach (var station in mapStations) {
			int score = _globalScore.GetScore(station.name);
			station.SetCompletionLevel(score);
			if (station.name == "Map_A.01") {
				station.SetCompletionLevel(0);
			}
		}
	}

	public static void SwitchScene(string name) {
		Scene cinematicScene = SceneManager.GetSceneByName("Cinematic_" + name);
		Scene gameScene = SceneManager.GetSceneByName(name);

		if (name == "Map_A.01") {
			SceneManager.LoadScene("Cinematic_" + name);
		}

		Debug.Log("Veut ouvrir " + name);
		if (cinematicScene.IsValid()) {
			Debug.Log("Ouvre Cinématique");
			SceneManager.LoadScene("Cinematic_" + name);
		} else if (gameScene.IsValid()) {
			Debug.Log("Ouvre Normal");
			SceneManager.LoadScene(name);
		} else {
			SceneManager.LoadScene(name);
		}
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
		CheckIfTutorialMustStart();
		UpdateStationsState();
	}
}
