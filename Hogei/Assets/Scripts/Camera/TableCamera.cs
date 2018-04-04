using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMapCamera : MonoBehaviour {

    [Header("Camera Settings")]
    public float CameraSpeed;

    [Header("Debug Settings")]
    public Transform DebugSphere;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 NewCamPos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
	}
}
