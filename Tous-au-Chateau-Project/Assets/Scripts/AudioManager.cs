using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;

    private bool _mute;
    private AudioSource source;
    public AudioClip mainMusic;

    // ***** SINGLETON *****/
    public static AudioManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                go.AddComponent<AudioManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        source = this.gameObject.AddComponent<AudioSource>();
        source.clip = mainMusic;
        source.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            ToggleSound();
        }
    }

    // MUTE | PLAY ALL SOUNDS
    public void ToggleSound()
    {
        _mute = !_mute;

        AudioListener.volume = (_mute) ? 0f : 1f;
    }

    public void ChangeMusic(AudioClip clip)
    {
        mainMusic = clip;
    }
}
