using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follow : MonoBehaviour {

    public Transform Target;
    public Vector3 FollowOffset;
    public Vector3 CameraDirection;
    public float AheadDistance;
    private Transform CameraTransform;
    private Vector3 CurrentDir;
	// Use this for initialization
	void Start () {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Target != null)
        {          
            Vector3 Dir = Target.GetComponent<Movement>().GetDirection();
            transform.position = Target.position;
            transform.position += Vector3.Scale(CameraDirection, FollowOffset);
            CurrentDir = Dir;
        }
	}
}
