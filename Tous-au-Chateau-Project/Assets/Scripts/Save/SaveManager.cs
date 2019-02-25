using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SaveManager {

	public static void Save(LevelScore newScore) {
		StreamReader reader = new StreamReader(Application.persistentDataPath + "/score.dat");

		string existingJSON = reader.ReadToEnd();
		reader.Close();

		var existingScores = JsonHelperList.FromJson<LevelScore>(existingJSON);

		// Remove the exisiting score if it exists for the current level
		for (int i = 0; i < existingScores.Count; ++i) {
			if (existingScores[i].levelName == newScore.levelName) {
				existingScores.Remove(existingScores[i]);
			}
		}

		// Add new score into the list
		existingScores.Add(newScore);
		string newData = JsonHelperList.ToJson(existingScores, true);

		StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/score.dat");

		writer.WriteLine(newData); // Rewrite score
		writer.Close();
	}

}
