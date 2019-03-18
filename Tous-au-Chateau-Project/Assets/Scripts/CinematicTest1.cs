using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTest1 : MonoBehaviour {

    public Animator transitionAnim;
    public Animator globalCloudsAnim;

    public Animator globalCameraAnim;
    public Animator globalCamera2Anim;
    public Animator chateauCameraAnim;
    public Animator villagersCameraAnim;

    private Camera _playerCamera; // set by CameraManager

    public Camera globalCamera;
    public Camera globalCamera2;
    public Camera chateauCamera;
    public Camera villagersCamera;

	// Use this for initialization
	void Start ()
    {
        /*globalCamera.enabled = false;
        globalCamera2.enabled = false;
        chateauCamera.enabled = false;
        villagersCamera.enabled = false;*/

        globalCamera.gameObject.SetActive(false);
        globalCamera2.gameObject.SetActive(false);
        chateauCamera.gameObject.SetActive(false);
        villagersCamera.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C))
        {
            // start cinematic
            StartCoroutine(playCinematic());
        }
	}

    void changeCamera()
    {

    }

    IEnumerator playCinematic()
    {
        Debug.Log("START CINEMATIC");

        Debug.Log("CAMERA 1");
        // Plan 1 : global view: zoom in
        globalCamera.gameObject.SetActive(true);
        transitionAnim.SetTrigger("fadeOutWhite");
        globalCameraAnim.SetTrigger("zoomGlobal");
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("CAMERA 2");
        // Plan 2 : up view: traveling
        globalCamera.gameObject.SetActive(false);
        globalCamera2.gameObject.SetActive(true);
        transitionAnim.SetTrigger("fadeOutWhite");
        globalCamera2Anim.SetTrigger("travelingGlobal");
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("CAMERA 3");
        // Plan 3 : chateau: traveling down -> up
        globalCamera2.gameObject.SetActive(false);
        chateauCamera.gameObject.SetActive(true);
        transitionAnim.SetTrigger("fadeOutWhite");
        chateauCameraAnim.SetTrigger("travelingChateau");
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("CAMERA 4");
        // Plan 4 : villagers: traveling right -> left
        chateauCamera.gameObject.SetActive(false);
        villagersCamera.gameObject.SetActive(true);
        transitionAnim.SetTrigger("fadeOutWhite");
        villagersCameraAnim.SetTrigger("travelingVillagers");
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1.5f);

    }
}
