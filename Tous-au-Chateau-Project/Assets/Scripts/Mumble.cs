using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mumble : MonoBehaviour {

	private AudioSource _audioData;
	public AudioClip a;
	public AudioClip b;
	public AudioClip c;
	public AudioClip d;
	public AudioClip f;
	public AudioClip g;
	public AudioClip h;
	public AudioClip i;
	public AudioClip j;
	public AudioClip l;
	public AudioClip m;
	public AudioClip n;
	public AudioClip o;
	public AudioClip p;
	public AudioClip q;
	public AudioClip r;
	public AudioClip s;
	public AudioClip t;
	public AudioClip u;
	public AudioClip v;
	public AudioClip w;
	public AudioClip x;
	public AudioClip y;
	public AudioClip space;

	// Use this for initialization
	void Start () {
		_audioData = GetComponent<AudioSource>();
	}

	private IEnumerator PlayText(string text) {
        _audioData.volume = 0.4f;
        var previousLetter = ' ';
		foreach (var letter in text) {
			if (letter != previousLetter) {
				if (letter == 'a') {
					_audioData.clip = a;
				}
				else if (letter == 'b') {
					_audioData.clip = b;
				}
				else if (letter == 'c') {
					_audioData.clip = c;
				}
				else if (letter == 'd') {
					_audioData.clip = d;
				}
				else if (letter == 'e') {
					_audioData.clip = d;
				}
				else if (letter == 'f') {
					_audioData.clip = f;
				}
				else if (letter == 'g') {
					_audioData.clip = g;
				}
				else if (letter == 'h') {
					_audioData.clip = h;
				}
				else if (letter == 'i') {
					_audioData.clip = i;
				}
				else if (letter == 'j') {
					_audioData.clip = j;
				}
				else if (letter == 'k') {
					_audioData.clip = a;
				}
				else if (letter == 'l') {
					_audioData.clip = l;
				}
				else if (letter == 'm') {
					_audioData.clip = m;
				}
				else if (letter == 'n') {
					_audioData.clip = n;
				}
				else if (letter == 'o') {
					_audioData.clip = o;
				}
				else if (letter == 'p') {
					_audioData.clip = p;
				}
				else if (letter == 'q') {
					_audioData.clip = q;
				}
				else if (letter == 'r') {
					_audioData.clip = r;
				}
				else if (letter == 's') {
					_audioData.clip = s;
				}
				else if (letter == 't') {
					_audioData.clip = t;
				}
				else if (letter == 'u') {
					_audioData.clip = u;
				}
				else if (letter == 'v') {
					_audioData.clip = v;
				}
				else if (letter == 'w') {
					_audioData.clip = w;
				}
				else if (letter == 'x') {
					_audioData.clip = x;
				}
				else if (letter == 'y') {
					_audioData.clip = y;
				}
				else if (letter == 'z') {
					_audioData.clip = b;
				}
				else if (letter == '!' || letter == ',' || letter == '.' || letter == '?') {
					_audioData.clip = r;
				}
				else {
					_audioData.clip = null;
				}
				if (_audioData.clip != null) {
					_audioData.Play();
					yield return new WaitForSeconds(0.05f);
				}
			}
				previousLetter = letter;
				yield return 0;

		}
        _audioData.volume = 1f;
    }

	public void Play(string text) {
		StartCoroutine(PlayText(text));
	}
}
