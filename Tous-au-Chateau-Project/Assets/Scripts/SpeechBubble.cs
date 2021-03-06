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
	private GameObject _textWithControllers;
	private GameObject _dots;
	private Text _textComp;
	private Text _textWithControllersComp;
	private bool _open = false;
	private Animator animator;
	private bool _isCameraDefault = false;
	private GameObject _controllerAnimation = null;
	private float minimalPanelSize = 500;
	private bool bold = false;
	private Mumble mumble;

	private AudioSource _audioData;
	public AudioClip bubbleSound;

	public float letterTime = 0.025f;

	public bool canClose = false;
	public bool dots = true;


	void Start()
	{
		mumble = GetComponent<Mumble>();
		cameraTransform = CameraManager.Instance.GetCameraTransform();
		_panel = this.transform.Find("Panel").gameObject;
		_text = this.transform.Find("Text").gameObject;
		_textWithControllers = this.transform.Find("TextWithControllers").gameObject;
		_dots = this.transform.Find("Dots").gameObject;
		_textComp = _text.GetComponent<Text>();
		_textWithControllersComp = _textWithControllers.GetComponent<Text>();
		_textComp.text = "";
		_textWithControllersComp.text = "";
		_audioData = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		AdaptCanvasToText();
		if (dots) {
			_dots.gameObject.SetActive(true);
		}
		if (_controllerAnimation != null) {
			_text.gameObject.SetActive(false);
			_textWithControllers.gameObject.SetActive(true);
		} else {
			_text.gameObject.SetActive(true);
			_textWithControllers.gameObject.SetActive(false);
		}
	}

	private void AdaptCanvasToText()
	{
		var panelRectTransform = _panel.GetComponent<RectTransform>();
		var textRectTransform = _text.GetComponent<RectTransform>();
		var size = _textComp.fontSize * _textComp.lineSpacing * message.Length / 32f;

		if (_controllerAnimation != null) {
			textRectTransform = _textWithControllers.GetComponent<RectTransform>();
			size = _textWithControllersComp.fontSize * _textWithControllersComp.lineSpacing * message.Length * 1.4f / 32f;
			if (size < minimalPanelSize) {
				size = minimalPanelSize;
			}
		}

		panelRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size + 10);
		textRectTransform.SetSizeWithCurrentAnchors(UnityEngine.RectTransform.Axis.Vertical, size);
	}

	void Update()
	{
		if (!cameraTransform) {
			cameraTransform = CameraManager.Instance.GetCameraTransform();
		} else {
			_panel.transform.rotation = Quaternion.LookRotation(_panel.transform.position - cameraTransform.position);
			_text.transform.rotation = Quaternion.LookRotation(_text.transform.position - cameraTransform.position);
			_textWithControllers.transform.rotation = Quaternion.LookRotation(_textWithControllersComp.transform.position - cameraTransform.position);
			if (CameraManager.Instance.IsCameraDefault(cameraTransform)) {
				cameraTransform = CameraManager.Instance.GetCameraTransform();
			}
		}
	}

	public void StartAnimation()
	{
		AdaptCanvasToText();
		_audioData.clip = bubbleSound;
		_audioData.Play();
		mumble.Play(message);
		StartCoroutine(AnimateText());
	}

	public bool CanClose()
	{
		return canClose;
	}

	public IEnumerator AnimateText()
	{
		if (_controllerAnimation != null) {
			_textWithControllersComp.text = "";
			yield return new WaitForSeconds(1);
			foreach (char letter in message)
			{
				string displayLetter = letter.ToString();
				if (letter == '#') {
						bold = !bold;
				} else {
					if (bold) {
					displayLetter = "<b><color=orange>" + letter + "</color></b>";
					}
					_textWithControllersComp.text += displayLetter;
				}
				yield return 0;
				yield return new WaitForSeconds(letterTime);
			}
			canClose = true;
		}
		else {
			_textComp.text = "";
			yield return new WaitForSeconds(1);
			foreach (char letter in message)
			{
				string displayLetter = letter.ToString();
				if (letter == '#') {
						bold = !bold;
				} else {
					if (bold) {
					displayLetter = "<b><color=orange>" + letter + "</color></b>";
					}
					_textComp.text += displayLetter;
				}
				yield return 0;
				yield return new WaitForSeconds(letterTime);
			}
			canClose = true;
		}
	}

	public void SetMessage(string text) {
		message = text;
		// AdaptCanvasToText();
	}

	public void SetControllerAnimation(GameObject controllerAnimation) {
		_text.gameObject.SetActive(false);
		_textWithControllers.gameObject.SetActive(true);
		if (controllerAnimation != null) {
			_controllerAnimation = controllerAnimation;
			_controllerAnimation.SetActive(true);
		}
	}

	public void UnsetControllerAnimation() {
		_text.gameObject.SetActive(true);
		_textWithControllers.gameObject.SetActive(false);
		if (_controllerAnimation != null) {
			_controllerAnimation.SetActive(false);
			_controllerAnimation = null;
		}
	}
}
