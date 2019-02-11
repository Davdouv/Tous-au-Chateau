using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour {

	public SpeechEvent[] events;
	private Animator _anim;
	private bool _canDisable = false;
  private int _openParameterId;
  const string _openTransitionName = "Open";
  const string _closedStateName = "Closed";

	void Update() {

		foreach (var evento in events)
		{
			if (!evento.IsOpen() && evento.MustOpen())
			{
				evento.SetIsOpen(true);
				_anim = evento._bubble.GetComponent<Animator>();
				_anim.SetBool(_openTransitionName, true);
				Open(evento._bubble);
			} else if (evento.IsOpen() && evento.MustClose())
			{
				evento.SetIsOpen(false);
				evento.SetIsDone(true);
				Close(evento._bubble);
			}
		}
	}

  void Start()
  {
      //We cache the Hash to the "Open" Parameter, so we can feed to Animator.SetBool.
      _openParameterId = Animator.StringToHash(_openTransitionName);
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
		}
	}

  IEnumerator DisablePanel(SpeechBubble bubble)
  {
			var currentClipInfo = _anim.GetCurrentAnimatorClipInfo(0);
			yield return new WaitForSeconds(3);
			_canDisable = true;
  }

}
