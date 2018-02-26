using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform Target;
    public Vector3 FollowOffset;
    public Vector3 CameraDirection;
    private Transform CameraTransform;
	// Use this for initialization
	void Start () {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Target != null)
        {
            transform.position = Target.position;
            transform.position += Vector3.Scale(CameraDirection, FollowOffset);
        }
	}
}
