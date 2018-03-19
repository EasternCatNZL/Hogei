using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitMoveBehavior : MonoBehaviour {

    [Header("Movement vars")]
    [Tooltip("Movement speed of the object")]
    public float moveSpeed = 5.0f;
    [Tooltip("Travel point a")]
    public Vector3 travelPointA = Vector3.zero;
    [Tooltip("Travel point b")]
    public Vector3 travelPointB = Vector3.zero;

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 3.0f;

    //control vars
    private bool isActive = false; //check if object active

    private Vector3 currentDestination = Vector3.zero;
    private Vector3 travelDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Move between points
    private void MoveBetweenPoints()
    {
        //if current direction does not exist <- zero, set
        if(travelDirection == Vector3.zero)
        {
            travelDirection = currentDestination - transform.position;
        }
        //move towards current destination
        transform.position += transform.position + ((travelDirection * moveSpeed) * Time.deltaTime);
    }

    //change destination
    public void ChangeDestination(Vector3 newDest)
    {
        currentDestination = newDest;
    }
}
