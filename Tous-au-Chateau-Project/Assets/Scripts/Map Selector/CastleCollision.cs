using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCollision : MonoBehaviour
{

    private string _tag = "Villager";
    private string _levelName;
    public Animator fadeAnimation;
    private bool _playTransition;

    void Start()
    {
        var mapStation = this.transform.parent.gameObject.GetComponent<MapStation>();
        _levelName = mapStation.name;
        _playTransition = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _tag)
        {
            Debug.Log("Collision with " + _levelName);

            // fade in transition
            if (fadeAnimation && _playTransition)
            {
                _playTransition = false;
                StartCoroutine(SceneTransition());
            }
        }
    }

    IEnumerator SceneTransition()
    {
        fadeAnimation.SetTrigger("SceneTransition");
        yield return new WaitForSeconds(1.5f);
    }
}