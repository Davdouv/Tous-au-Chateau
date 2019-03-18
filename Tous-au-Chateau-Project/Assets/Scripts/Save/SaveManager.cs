using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// Data saved in [user]/AppData/LocalLow/DefaultCompany/Tous-au-Chateau-Project

public static class SaveManager {

	public static void Save(LevelScore newScore) {

        string path = Application.persistentDataPath + "/score.dat";

        Debug.Log(path);

        // If file doesn't exist, create it
        if (!IsFileValid(path))
        {
            StreamWriter temp_writer = new StreamWriter(path);
            temp_writer.Close();
        }

		StreamReader reader = new StreamReader(path);

		string existingJSON = reader.ReadToEnd();
		reader.Close();

		var existingScores = JsonHelperList.FromJson<LevelScore>(existingJSON);

        if (existingScores != null)
        {
            // Remove the exisiting score if it exists for the current level
            for (int i = 0; i < existingScores.Count; ++i)
            {
                if (existingScores[i].levelName == newScore.levelName)
                {
                    if (existingScores[i].stars < newScore.stars)
                    {
                        existingScores.Remove(existingScores[i]); // Remove precedent score
                    }
                    else
                    {
                        newScore = existingScores[i];
                    }
                }
            }
        }
        else
        {
            existingScores = new List<LevelScore>();
        }

		existingScores.Add(newScore); // Add new score into the list

		string newData = JsonHelperList.ToJson(existingScores, true);

		StreamWriter writer = new StreamWriter(path);

		writer.WriteLine(newData); // Rewrite score
		writer.Close();
    }

    private static bool IsFileValid(string filePath)
    {
        bool IsValid = true;

        if (!File.Exists(filePath))
        {
            IsValid = false;
        }
        else if (Path.GetExtension(filePath).ToLower() != ".dat")
        {
            IsValid = false;
        }

        return IsValid;
    }

}
