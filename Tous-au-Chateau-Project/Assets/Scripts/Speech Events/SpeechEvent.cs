using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEvent : MonoBehaviour {

	[TextArea(3,10)]
	public string _message;
	public SpeechBubble _bubble;
	public bool _isOpen = false;
	public bool _isDone = false;
	public bool ouvrir = false;
	public SpeechEvent _previousEvent;

	// Use this for initialization
	void Start () {
		_bubble.setMessage(_message);
	}

	public virtual bool MustOpen() { return ouvrir; }

	public virtual bool MustClose() { return !ouvrir; }

	public bool IsOpen() { return _isOpen; }
	public void SetIsOpen(bool bo) { _isOpen = bo; }
	public void SetIsDone(bool bo) { _isDone = bo; }

	// Update is called once per frame
	void Update () {

	}
}
