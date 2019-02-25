using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechEvent : MonoBehaviour {

	[TextArea(3,10)]
	public string message;
	public SpeechBubble bubble;
	private bool _isOpen = false;
	private bool _isDone = false;
	public SpeechEvent previousEvent;

    public bool hasDoneAction;  // For events that need to detect if player has done an action

    public static VillagersGroup currentVillagersGroup;

	// Use this for initialization
	void Start () {
		// bubble.setMessage(message);
	}

	public virtual bool MustOpen() { return false; }

	public virtual bool MustClose() { return false; }

	public bool IsOpen() { return _isOpen; }
	public bool IsDone() { return _isDone; }
	public void SetIsOpen(bool bo) {
		if (bo) {
			bubble.setMessage(message);
		}
		_isOpen = bo;
	}
	public void SetIsDone(bool bo) { _isDone = bo; }
}
