using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour {

	public static SceneTransitions Instance;
    public Animator transitionAnim;

	public string scene1Name;
	public string scene2Name;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {
			
		}

		if (Input.GetKeyDown(KeyCode.A)) {

		}

        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(LoadScene("end", scene2Name));
        }
	}

    IEnumerator LoadScene(string animName, string sceneName)
    {
        transitionAnim.SetTrigger(animName);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
