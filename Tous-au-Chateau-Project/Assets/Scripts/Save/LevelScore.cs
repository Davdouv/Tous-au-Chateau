using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class LevelScore {

	public string levelName;
	public int remainingVillagers;
	public float duration;

	public int villagersCeil1 = 1; // Number of villagers necessary for lvl 1
	public int villagersCeil2 = 5; // ~ lvl 2
	public int villagersCeil3 = 10; // ~ lvl 3

	public float durationCeil1 = 100f; // Greatest duration allowed for lvl 1
	public float durationCeil2 = 0.5f; // ~ lvl 2
	public float durationCeil3 = 0.2f; // ~ lvl 3

	public int ComputeScore() {
		int score = 0;
		if (remainingVillagers >= villagersCeil3 && duration <= durationCeil3) {
			score = 3;
		}
		else if (remainingVillagers >= villagersCeil2 && duration <= durationCeil2) {
			score = 2;
		}
		else if (remainingVillagers >= villagersCeil1 && duration <= durationCeil1) {
			score = 1;
		}
		return score;
	}
}
