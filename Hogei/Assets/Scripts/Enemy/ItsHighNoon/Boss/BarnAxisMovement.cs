using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnAxisMovement : MonoBehaviour {

    [Header("Rotation vars")]
    [Tooltip("Rotation speed")]
    public float rotationSpeed = 20.0f;

    //control vars
    public bool doRotation = false; //check if rotation should be occuring

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (doRotation)
        {
            RotateAxis();
        }
	}

    //Rotation logic
    private void RotateAxis()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }
}
