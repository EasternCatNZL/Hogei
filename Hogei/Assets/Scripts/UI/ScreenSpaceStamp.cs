using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceStamp : MonoBehaviour {

    [Header("Screenspace Coords: As a percentage")]
    public Vector3 SSCoords = Vector3.zero;

	// Use this for initialization
	void Start () {
        Vector3 temp = new Vector3(Screen.width * SSCoords.x, Screen.height * SSCoords.y, SSCoords.z);
        Vector3 WSCoords = Camera.main.ScreenToWorldPoint(temp);
        transform.position = WSCoords;
    }

    void LateUpdate()
    {
        Vector3 temp = new Vector3(Screen.width * SSCoords.x, Screen.height * SSCoords.y, SSCoords.z);
        Vector3 WSCoords = Camera.main.ScreenToWorldPoint(temp);
        transform.position = WSCoords;
    }
}
