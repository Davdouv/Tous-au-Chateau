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
	private bool _isCameraDefault = false;


	public bool canClose = false;
	public bool dots = true;


	void Start()
	{
		cameraTransform = CameraManager.Instance.GetCamera().transform;
		_panel = this.transform.Find("Panel").gameObject;
		_text = this.transform.Find("Text").gameObject;
		_dots = this.transform.Find("Dots").gameObject;
		_textComp = _text.GetComponent<Text>();
		_textComp.text = "";
		animator = GetComponent<Animator>();
		AdaptCanvasToText();
		if (dots) {
			_dots.gameObject.SetActive(true);
		}
	}

	// public void OnEnable()
  // {
	// 	FindCamera();
	// 	_panel = this.transform.Find("Panel").gameObject;
	// 	_text = this.transform.Find("Text").gameObject;
	// 	_dots = this.transform.Find("Dots").gameObject;
	// 	_textComp = _text.GetComponent<Text>();
	// 	_textComp.text = "";
	// 	animator = GetComponent<Animator>();
	// 	AdaptCanvasToText();
  // }

	private void AdaptCanvasToText()
	{
		var panelRectTransform = _panel.GetComponent<RectTransform>();
		var textRectTransform = _text.GetComponent<RectTransform>();
		var size = _textComp.fontSize * _textComp.lineSpacing * message.Length;
		panelRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size);
		textRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size + 10);
	}

	private bool FindCamera() {
		if (GameObject.Find("Neck/Camera")) { // VR
			Debug.Log("VR Camera");
			_isCameraDefault = false;
			cameraTransform = GameObject.Find("Neck/Camera").transform;
			return true;
		} else if (GameObject.Find("[VRSimulator_CameraRig]")) { // Simulator
			Debug.Log("Simulator Camera");
			_isCameraDefault = false;
			cameraTransform = GameObject.Find("Camera (eye)").transform;
			return true;
		} else if (GameObject.Find("Main Camera")) { // Default
			Debug.Log("Default Camera");
			_isCameraDefault = true;
			cameraTransform = GameObject.Find("Main Camera").transform;
			return true;
		}
		return false;
	}

	void Update()
	{
		if (!cameraTransform) {
			cameraTransform = CameraManager.Instance.GetCamera().transform;
		} else {
			_panel.transform.rotation = Quaternion.LookRotation(_panel.transform.position - cameraTransform.position);
			_text.transform.rotation = Quaternion.LookRotation(_text.transform.position - cameraTransform.position);
			if (CameraManager.Instance.IsCameraDefault()) {
				Debug.Log("Default");
				cameraTransform = CameraManager.Instance.GetCamera().transform;
			}
		}
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
