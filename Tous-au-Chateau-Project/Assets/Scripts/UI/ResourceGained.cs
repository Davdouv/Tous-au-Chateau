using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGained : MonoBehaviour {

    public float animationTime = 1.0f;

    private Text _gainText;
    private float _startTime;
    private Transform _cameraTransform;

    bool init = false;

    private Vector3 _startPos;

	// Use this for initialization
	void Start () {
        if (!init)
        {
            init = true;
            Debug.Log(transform.childCount);
            _gainText = transform.GetChild(0).GetComponent<Text>();
            _startTime = Time.time;
            _startPos = _gainText.rectTransform.position;

            _cameraTransform = CameraManager.Instance.GetCameraTransform();
            Invoke("SelfDestroy", animationTime + 1.0f);
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
        //An object can't give several resources at the same time
        //Thus, the result of the addition only returns the right resource number
        int gainedNb = gained.food + gained.wood + gained.stone + gained.workForce + gained.motivation;

        _gainText.text = "" + gainedNb;
    }
}
