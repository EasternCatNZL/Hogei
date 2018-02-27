using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraBehavior : MonoBehaviour {

    [Header("Camera speed")]
    [Tooltip("Speed of camera follow")]
    public float cameraSpeed = 20.0f;

    [Header("Camera offset")]
    [Tooltip("The offset of the camera")]
    public float cameraOffset = 20.0f;

    [Header("Timing vars")]
    [Tooltip("Time taken for camera follow")]
    public float cameraMoveTime = 2.0f;

    //control vars
    [HideInInspector]
    public float lastMoveTime = 0.0f; //time player last moved cursor point

    private Vector3 lastPoint = Vector2.zero; //Point camera was at before player changes point
    private Vector3 newPoint = Vector2.zero; //Point camera is to move to

    //lerp vars
    float distance = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > lastMoveTime + cameraMoveTime)
        {
            MoveCameraAfterSelection();
        }
	}

    //Setup for movement
    public void SetupMovement(Vector2 newLocation)
    {
        lastPoint = transform.position;
        newPoint = newLocation;
        distance = Vector2.Distance(newPoint, lastPoint);
        lastMoveTime = Time.time;
    }

    //Move the camera after player selection
    void MoveCameraAfterSelection()
    {
        //lerp towards location <- doesnt need to be perfect
        float distCovered = (Time.time - lastMoveTime) * cameraSpeed;
        float fracJourney = distCovered / distance;
        transform.position = Vector2.Lerp(lastPoint, newPoint, fracJourney);
        //offset the camera
        transform.position = new Vector3(transform.position.x, transform.position.y, -cameraOffset);
    }
}
