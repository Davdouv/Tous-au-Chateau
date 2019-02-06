using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {

	Transform cameraTransform = null;
	[TextArea(3,10)]
	public string message;
	private GameObject _panel;
	private GameObject _text;
	private GameObject _dots;
	private Text _textComp;
	private float letterTime = 0.06f;
	private bool _open = false;
	private Animator animator;
	private bool canClose = false;

	public bool debug_hide = false;

	void Start()
	{
		FindCamera();
		_panel = this.transform.Find("Panel").gameObject;
		_text = this.transform.Find("Text").gameObject;
		_dots = this.transform.Find("Dots").gameObject;
		_textComp = _text.GetComponent<Text>();
		_textComp.text = "";
		animator = GetComponent<Animator>();
		AdaptCanvasToText();
	}

	public void OnEnable()
  {
		FindCamera();
		_panel = this.transform.Find("Panel").gameObject;
		_text = this.transform.Find("Text").gameObject;
		_dots = this.transform.Find("Dots").gameObject;
		_textComp = _text.GetComponent<Text>();
		_textComp.text = "";
		animator = GetComponent<Animator>();
		AdaptCanvasToText();
  }

	private void AdaptCanvasToText()
	{
		var panelRectTransform = _panel.GetComponent<RectTransform>();
		var textRectTransform = _text.GetComponent<RectTransform>();
		var size = _textComp.fontSize * _textComp.lineSpacing * message.Length / 25;
		panelRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size);
		textRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size + 10);
	}

	private bool FindCamera() {
		if (GameObject.Find("Neck/Camera")) {
			cameraTransform = GameObject.Find("Neck/Camera").transform;
			return true;
		} else if (GameObject.Find("Camera (eye)")) {
			cameraTransform = GameObject.Find("Camera (eye)").transform;
			return true;
		}
		return false;
	}

	void Update()
	{
		if (!cameraTransform) {
			FindCamera();
		}
		_panel.transform.rotation = Quaternion.LookRotation(_panel.transform.position - cameraTransform.position);
		_text.transform.rotation = Quaternion.LookRotation(_text.transform.position - cameraTransform.position);
	}

	public void StartAnimation()
	{
		StartCoroutine(AnimateText());
	}

	public bool CanClose()
	{
		return canClose;
	}

	public IEnumerator AnimateText()
	{
		yield return new WaitForSeconds(1);
		_textComp.text = "";
		foreach (char letter in message)
		{
			_textComp.text += letter;
			yield return 0;
			yield return new WaitForSeconds(letterTime);
		}
		canClose = true;
	}

	public void setMessage(string text) {
		message = text;
	}
}
