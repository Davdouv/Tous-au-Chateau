using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTest1 : MonoBehaviour {

    public Animator transitionAnim;
    public Animator globalCloudsAnim;

    public Camera tmpCamera;

    public Animator globalCameraAnim;
    public Animator globalCamera2Anim;
    public Animator chateauCameraAnim;
    public Animator villagersCameraAnim;

    private Camera _playerCamera; // set by CameraManager
    private Camera _copyPlayerCamera;

    public Camera globalCamera;
    public Camera globalCamera2;
    public Camera chateauCamera;
    public Camera villagersCamera;

    public bool shouldStart;

	// Use this for initialization
	void Start ()
    {
        globalCamera.enabled = false;
        globalCamera2.enabled = false;
        chateauCamera.enabled = false;
        villagersCamera.enabled = false;

        shouldStart = true;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C))
        {
            if (shouldStart)
            {
                Debug.Log("Should Start");
                StartCoroutine(PlayCinematic());
            }
        }
	}

    public void StartCinematic(Camera player)
    {
        shouldStart = true;
        _playerCamera = tmpCamera;
        _copyPlayerCamera = player;
        
        Debug.Log("______________________________________________________________________________CAMERA IS : " + player.gameObject.name);
    }

    void ChangeCamera(Camera currentCamera, Camera newCamera)
    {
        currentCamera.enabled = false;
        newCamera.enabled = true;
    }

    IEnumerator PlayCinematic()
    {
        Debug.Log("PLAY CINEMATIC");

        Debug.Log("CAMERA 1");
        // Plan 1 : global view: zoom in
        transitionAnim.SetTrigger("fadeOutWhite");
        globalCloudsAnim.SetTrigger("cloudsFall");
        globalCameraAnim.SetTrigger("zoomGlobal");
        ChangeCamera(tmpCamera, globalCamera);
        yield return new WaitForSeconds(5f);

        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1f);

        Debug.Log("CAMERA 2");
        // Plan 2 : up view: traveling
        transitionAnim.SetTrigger("fadeOutWhite");
        globalCamera2Anim.SetTrigger("travelingGlobal");
        ChangeCamera(globalCamera, globalCamera2);
        yield return new WaitForSeconds(8f);

        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1f);

        Debug.Log("CAMERA 3");
        // Plan 3 : chateau: traveling down -> up
        transitionAnim.SetTrigger("fadeOutWhite");
        chateauCameraAnim.SetTrigger("travelingChateau");
        ChangeCamera(globalCamera2, chateauCamera);
        yield return new WaitForSeconds(6f);

        transitionAnim.SetTrigger("fadeInWhite");
        yield return new WaitForSeconds(1f);

        //Debug.Log("CAMERA 4");
        // Plan 4 : villagers: traveling right -> left
        //villagersCameraAnim.SetTrigger("travelingVillagers");
        //ChangeCamera(chateauCamera, villagersCamera);
        //transitionAnim.SetTrigger("fadeOutWhite");
        //yield return new WaitForSeconds(5f);
        //transitionAnim.SetTrigger("fadeInWhite");
        //yield return new WaitForSeconds(1f);

        ChangeCamera(villagersCamera, tmpCamera);
    }
}
