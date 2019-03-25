using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//IMPORTANT LEAVE THIS LINE HERE
//VISUAL STUDIO DOESN'T UNDERSTANDS IT
//BUT IT HAS IMPACT ON THE SCRIPT
using TMPro;

public class ResourceGained : MonoBehaviour {

    public float animationTime = 1.0f;

    //IMPORTANT THIS LINE IS NOT AN ERROR
    //VISUAL STUDIO DOESN'T UNDERSTANDS IT BUT IT WORKS
    private TextMeshProUGUI _gainText;
    private float _startTime;
    private Transform _cameraTransform;

    bool init = false;

    private Vector3 _startPos;

	// Use this for initialization
	void Start () {
        if (!init)
        {
            init = true;
            //Debug.Log("into start function of resource gained script : " + transform.childCount);
            _gainText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _startTime = Time.time;
            _startPos = _gainText.rectTransform.position;

            _cameraTransform = CameraManager.Instance.GetCameraTransform();
            Invoke("SelfDestroy", animationTime + 2.0f);
        }
    }

	// Update is called once per frame
	void Update () {
        float timeElapsed = (Time.time - _startTime);
        float fracJourney = timeElapsed / animationTime;

        //Position
        Vector3 self = _gainText.rectTransform.position;
        self.y = Mathf.Lerp(_startPos.y, _startPos.y + 5.0f, fracJourney);
        _gainText.rectTransform.position = self;

        //Text alpha
        Color c = _gainText.color;
        c.a = Mathf.Lerp(0, 1, fracJourney);
        _gainText.color = c;

        //Look at player
        if (CameraManager.Instance.IsCameraDefault(_cameraTransform))
        {
          _cameraTransform = CameraManager.Instance.GetCameraTransform();
        }
        transform.LookAt(_cameraTransform);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public void UpdateGain(ResourcesPack gained)
    {
        if (!init)
        {
            Start();
        }

        //Debug.Log("In updated gain of resource gain script");

        if (gained.wood > 0)
        {
            _gainText.text = "+" + gained.wood + "<sprite=0>";
            return;
        }

        if (gained.stone > 0)
        {
            _gainText.text = "+" + gained.stone + "<sprite=1>";
            return;
        }

        if (gained.food > 0)
        {
            _gainText.text = "+" + gained.food + "<sprite=2>";
            return;
        }

        if (gained.workForce > 0)
        {
            _gainText.text = "+" + gained.workForce + "<sprite=3>";
            return;
        }

        if (gained.motivation > 0)
        {
            _gainText.text = "+" + gained.motivation + "<sprite=4>";
            return;
        }

        _gainText.text = "+0";
    }
}
