using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    //Forward and Up vector to look towards the camera
    private Vector3 Forward;
    private Vector3 Up;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void LateUpdate () {
        Forward = Vector3.Cross(Camera.main.transform.up, Camera.main.transform.right);
        //Up = Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.right);
        Up = Camera.main.transform.up;
        transform.rotation = Quaternion.LookRotation(Forward,Up);
    }
}
