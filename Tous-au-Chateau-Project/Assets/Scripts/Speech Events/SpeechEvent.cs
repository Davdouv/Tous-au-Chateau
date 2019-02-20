using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEvent : MonoBehaviour {

	[TextArea(3,10)]
	public string _message;
	public SpeechBubble _bubble;
	private bool _isOpen = false;
	private bool _isDone = false;
	public SpeechEvent _previousEvent;

    public static VillagersGroup currentVillagersGroup;

	// Use this for initialization
	void Start () {
		_bubble.setMessage(_message);
	}

	public virtual bool MustOpen() { return false; }

	public virtual bool MustClose() { return false; }

	public bool IsOpen() { return _isOpen; }
	public bool IsDone() { return _isDone; }
	public void SetIsOpen(bool bo) { _isOpen = bo; }
	public void SetIsDone(bool bo) { _isDone = bo; }

	// Update is called once per frame
	void Update () {

	}
}
