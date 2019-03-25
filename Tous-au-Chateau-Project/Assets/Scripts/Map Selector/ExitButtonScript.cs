using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour {

    public void ExitGame()
    {
        GetComponent<Animator>().enabled = true;
        StartCoroutine(End());

    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
