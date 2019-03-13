using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class LevelScore {

	public string levelName;
	public int stars;

	private int _remainingVillagers = 0;
	private float _duration = 0;

	private int _villagersCeil1 = 1; // Number of villagers necessary for lvl 1
	private int _villagersCeil2 = 5; // ~ lvl 2
	private int _villagersCeil3 = 10; // ~ lvl 3

	private float _durationCeil1 = 100f; // Greatest duration allowed for lvl 1
	private float _durationCeil2 = 0.5f; // ~ lvl 2
	private float _durationCeil3 = 0.2f; // ~ lvl 3

	// Constructor with custom ceils
	public LevelScore(string name, int remainingVillagers, float duration, int[] villagersCeils, float[] durationCeils) {
		levelName = name;
		_remainingVillagers = remainingVillagers;
		_villagersCeil1 = villagersCeils[0];
		_villagersCeil2 = villagersCeils[1];
		_villagersCeil3 = villagersCeils[2];
		_durationCeil1 = durationCeils[0];
		_durationCeil2 = durationCeils[1];
		_durationCeil3 = durationCeils[2];
		stars = ComputeScore();
	}

	// Constructor with default ceils
	public LevelScore(string name, int remainingVillagers, float duration) {
		levelName = name;
		_remainingVillagers = remainingVillagers;
		stars = ComputeScore();
	}

	// Return the number of stars corresponding to the player's performance
	private int ComputeScore() {
		int score = 0;
		if (_remainingVillagers >= _villagersCeil3 && _duration <= _durationCeil3) {
			score = 3;
		}
		else if (_remainingVillagers >= _villagersCeil2 && _duration <= _durationCeil2) {
			score = 2;
		}
		else if (_remainingVillagers >= _villagersCeil1 && _duration <= _durationCeil1) {
			score = 1;
		}
		return score;
	}
}
