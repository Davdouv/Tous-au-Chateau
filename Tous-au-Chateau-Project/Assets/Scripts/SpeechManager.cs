using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour {

	public bool showMessage;
	public SpeechBubble sb;
	public SpeechEvent[] events;
	private bool _previousState;
	private Animator _anim;
	private bool _canDisable = false;

	//Screen to open automatically at the start of the Scene
  // public Animator initiallyOpen;

  //Hash of the parameter we use to control the transitions.
  private int _openParameterId;

	//Animator State and Transition names we need to check against.
  const string _openTransitionName = "Open";
  const string _closedStateName = "Closed";

  //The GameObject Selected before we opened the current Screen.
  //Used when closing a Screen, so we can go back to the button that opened it.
  // private GameObject m_PreviouslySelected;

	void Update() {
		// if (_previousState != showMessage) {
		// 	if (showMessage)
		// 	{
		// 		Open(sb);
		// 	}
		// 	else if (!showMessage)
		// 	{
		// 		Close(sb);
		// 	}
		// }
		// if (!showMessage && _canDisable)
		// 	sb.gameObject.SetActive(false);
		//
		// _previousState = showMessage;

		foreach (var evento in events)
		{
			if (!evento.IsOpen() && evento.CanOpen())
			{
				evento.SetIsOpen(true);
				Open(evento.bubble);
			} else if (evento.IsOpen() && evento.CanClose())
			{
				evento.SetIsOpen(false);
				Close(evento.bubble);
			}
		}
	}

  void Start()
  {
			_previousState = showMessage;
			_anim = sb.GetComponent<Animator>();
      //We cache the Hash to the "Open" Parameter, so we can feed to Animator.SetBool.
      _openParameterId = Animator.StringToHash(_openTransitionName);
			_anim.SetBool(_openTransitionName, true);
  }

	private void Open(SpeechBubble bubble) {
		bubble.gameObject.SetActive(true);
		_anim.SetBool(_openTransitionName, true);
		bubble.StartAnimation();
	}

	private void Close(SpeechBubble bubble) {
		if (bubble.CanClose())
		{
			_anim.SetBool(_openTransitionName, false);
			StartCoroutine(DisablePanel(bubble));
		} else {
			Debug.Log("Animation is still playing.");
			showMessage = true;
		}
	}

  //Coroutine that will detect when the Closing _animation is finished and it will deactivate the
  //hierarchy.
  IEnumerator DisablePanel(SpeechBubble bubble)
  {
			var currentClipInfo = _anim.GetCurrentAnimatorClipInfo(0);
			yield return new WaitForSeconds(3);
			_canDisable = true;
  }

}
