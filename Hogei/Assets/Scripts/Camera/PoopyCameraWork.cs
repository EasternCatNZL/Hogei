using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopyCameraWork : MonoBehaviour {

    [Header("Camera movement")]
    public float xMove = 0.0f;
    public float yMove = 0.0f;
    public float zMove = 0.0f;
    public Vector3 moveVec = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = transform.position + (moveVec * Time.deltaTime);
        transform.position += (moveVec * Time.deltaTime);
    }
}
