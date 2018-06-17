using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopCameraArm : MonoBehaviour {

    [Header("Rotation vars")]
    [Tooltip("Rotation speed")]
    public float rotationSpeed = 20.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
