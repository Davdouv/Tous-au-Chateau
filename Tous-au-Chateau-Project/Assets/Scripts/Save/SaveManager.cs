using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SaveManager {

	public static void Save(LevelScore newScore) {
		StreamReader reader = new StreamReader(Application.persistentDataPath + "/score.dat");
		StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/score.dat");

		string existingJSON = reader.ReadToEnd();
		reader.Close();

		List<LevelScore> existingScores = JsonHelperList.FromJson<LevelScore>(existingJSON);

		// Remove the exisiting score if it exists for the current level
		foreach (var score in existingScores) {
			if (score.levelName == newScore.levelName) {
				existingScores.Remove(score);
			}
		}

		// Add new score into the list
		existingScores.Add(newScore);

		string newData = JsonHelperList.ToJson(existingScores, true);
		writer.WriteLine(newData); // Rewrite score
		writer.Close();
	}

}
