using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCollision : MonoBehaviour {

	private string _tag = "Villager";
	private string _levelName;

	void Start() {
		var mapStation = this.transform.parent.gameObject.GetComponent<MapStation>();
		_levelName = mapStation.name;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collision");
			if (collision.gameObject.tag == _tag)
			{
					Debug.Log(_levelName);
			}
	}
}
