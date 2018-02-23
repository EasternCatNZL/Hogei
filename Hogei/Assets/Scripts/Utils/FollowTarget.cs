using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform Target;
    public Transform CameraTransform;

	// Use this for initialization
	void Start () {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Target.position;
	}
}
