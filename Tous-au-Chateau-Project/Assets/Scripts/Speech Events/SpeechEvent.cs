using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEvent : MonoBehaviour {

	[TextArea(3,10)]
	public string message;
	public SpeechBubble bubble;
	private bool isOpen = false;
	public bool ouvrir = false;

	// Use this for initialization
	void Start () {
		bubble.setMessage(message);
	}

	public bool CanOpen() { return ouvrir; }

	public bool CanClose() { return !ouvrir; }

	public bool IsOpen() { return isOpen; }
	public void SetIsOpen(bool bo) { isOpen = bo; }

	// Update is called once per frame
	void Update () {

	}
}
