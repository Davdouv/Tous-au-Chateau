using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GlobalScore : MonoBehaviour {

	private List<LevelScore> _scores;
	public bool ready;
	public bool log = false;

	// Use this for initialization
	void Start () {
		_scores = new List<LevelScore>();

		// CreateScoreDebug(); // DEBUG

		ready = FetchData();

		if (log) {
			foreach (var score in _scores) {
				Debug.Log(score.levelName + " : " + score.stars);
			}
		}
	}

	// Update is called once per frame
	void Update () {


	}

	private bool FetchData() {
		if (File.Exists(Application.persistentDataPath + "/score.dat")) {
      //get content of json string
      StreamReader file = new StreamReader(Application.persistentDataPath + "/score.dat");
      string json = file.ReadToEnd();
      var loadedScores = JsonHelperList.FromJson<LevelScore>(json);
      file.Close();
      //save the loaded data
      _scores = loadedScores;
			return true;
    }
		return false;
	}

	public int GetScore(string levelName) {
		foreach (var score in _scores) {
			if (score.levelName == levelName) {
				return score.stars;
			}
		}
		return -1;
	}

	private string ScoresToJSON() {
		bool prettyPrint = true;
    return JsonHelperList.ToJson(_scores, prettyPrint); //serializing the list using a custom JsonHelper, adapting JsonUtility for Lists
	}

	// DEBUG
	private void CreateFakeScores() {
		// Create Fake Scores
		// 3 stars
		var score1 = new LevelScore("Tutorial 1", 11, 0.1f);
		_scores.Add(score1);

		// 2 stars
		var score2 = new LevelScore("Tutorial 2", 9, 0.4f);
		_scores.Add(score2);

		// 3 stars
		var score3 = new LevelScore("Tutorial 3", 11, 0.13f);
		_scores.Add(score3);

		// 1 star
		var score4 = new LevelScore("Very Easy", 1, 0.4f);
		_scores.Add(score4);

		// 0 star
		var score5 = new LevelScore("Easy", 0, 0.13f);
		_scores.Add(score5);
	}

	// DEBUG
	private void CreateScoreDebug() {
		StreamWriter file = new StreamWriter(Application.persistentDataPath + "/score.dat");

		CreateFakeScores();

		file.WriteLine(ScoresToJSON());
		file.Close();
	}
}
