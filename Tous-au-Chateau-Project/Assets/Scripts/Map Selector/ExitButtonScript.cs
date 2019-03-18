using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<Animator>().enabled = true;
        StartCoroutine(End());

    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
