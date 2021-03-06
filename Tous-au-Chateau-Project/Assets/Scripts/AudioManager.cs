using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            // create logic to create the instance
            if (_instance == null)
            {
                GameObject go = new GameObject("_AudioManager");
                go.AddComponent<AudioManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    private static bool _mute;
    private AudioSource source;
    public AudioClip mainMusic;

    private void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();
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
