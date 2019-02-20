using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GlobalScore : MonoBehaviour {

	private List<LevelScore> _scores;

	// Use this for initialization
	void Start () {
		_scores = new List<LevelScore>();

		CreateScoreDebug();
		_scores = new List<LevelScore>();
		FetchData();
		Debug.Log(Application.persistentDataPath);
		foreach (var score in _scores) {
			Debug.Log(score.levelName + " : " + score.ComputeScore());
		}
	}

	// Update is called once per frame
	void Update () {

	}

	private void FetchData() {
		if (File.Exists(Application.persistentDataPath + "/score.dat")) {
      //get content of json string
      StreamReader file = new StreamReader(Application.persistentDataPath + "/score.dat");
      string json = file.ReadToEnd();
      var loadedScores = JsonHelperList.FromJson<LevelScore>(json);
      file.Close();
      //save the loaded data
      _scores = loadedScores;
    }
	}

	public int GetScore(string levelName) {
		foreach (var score in _scores) {
			if (score.levelName == levelName) {
				return score.ComputeScore();
			}
		}
		return -1;
	}

	private string scoresToJSON() {
		bool prettyPrint = true;
    return JsonHelperList.ToJson(_scores, prettyPrint); //serializing the list using a custom JsonHelper, adapting JsonUtility for Lists
	}

	// DEBUG
	private void CreateFakeScores() {
		// Create Fake Scores
		// 3 stars
		var score1 = new LevelScore();
		score1.remainingVillagers = 11;
		score1.duration = 0.1f;
		score1.levelName = "Tutorial 1";
		_scores.Add(score1);

		// 2 stars
		var score2 = new LevelScore();
		score2.remainingVillagers = 9;
		score2.duration = 0.4f;
		score2.levelName = "Tutorial 2";
		_scores.Add(score2);

		// 3 stars
		var score3 = new LevelScore();
		score3.remainingVillagers = 11;
		score3.duration = 0.13f;
		score3.levelName = "Tutorial 3";
		_scores.Add(score3);

		// 1 star
		var score4 = new LevelScore();
		score4.remainingVillagers = 1;
		score4.duration = 0.4f;
		score4.levelName = "Very Easy";
		_scores.Add(score4);

		// 0 star
		var score5 = new LevelScore();
		score5.remainingVillagers = 0;
		score5.duration = 0.13f;
		score5.levelName = "Easy";
		_scores.Add(score5);
	}

	// DEBUG
	private void CreateScoreDebug() {
		StreamWriter file = new StreamWriter(Application.persistentDataPath + "/score.dat");

		CreateFakeScores();

		file.WriteLine(scoresToJSON());
		file.Close();
	}
}
