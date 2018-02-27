using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraBehavior : MonoBehaviour {

    [Header("Camera speed")]
    [Tooltip("Speed of camera follow")]
    public float cameraSpeed = 20.0f;

    [Header("Timing vars")]
    [Tooltip("Time taken for camera follow")]
    public float cameraMoveTime = 2.0f;

    //control vars
    private float lastMoveTime = 0.0f; //time player last moved cursor point

    private Vector3 lastPoint = Vector3.zero; //Point camera was at before player changes point
    private Vector3 newPoint = Vector3.zero; //Point camera is to move to

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
