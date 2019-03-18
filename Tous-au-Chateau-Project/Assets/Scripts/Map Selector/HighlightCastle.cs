using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightCastle : MonoBehaviour {

    private MapSelector _mapSelector;

    private void Start()
    {
        _mapSelector = GameObject.Find("Map Selector").GetComponent<MapSelector>();
    }

    private void Update()
    {
        Highlight();
    }

    private void Highlight()
    {
        int layerMask = 1 << 12;

        RaycastHit hit;

        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 100, Color.red);

        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask))
        {
            _mapSelector.Highlight(hit.transform);
        }
        else
        {
            _mapSelector.DontHighlight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Castle")
        {
            Debug.Log("destroy");
            Destroy(this.gameObject);
        }
    }
}
