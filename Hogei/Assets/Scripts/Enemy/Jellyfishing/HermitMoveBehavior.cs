using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitMoveBehavior : MonoBehaviour {

    [Header("Movement vars")]
    [Tooltip("Movement speed of the object")]
    public float moveSpeed = 5.0f;
    [Tooltip("Travel points")]
    public HermitCheckPointHandler[] travelPoints = new HermitCheckPointHandler[0];

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 3.0f;

    //control vars
    private bool isActive = false; //check if object active

    private int currentIndex = 0; //the current index of the array

    private Vector3 currentDestination = Vector3.zero;
    private Vector3 travelDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
        SetupPointRefs();
        //debug
        currentDestination = travelPoints[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        MoveBetweenPoints();
	}

    //Set up refs for all points
    private void SetupPointRefs()
    {
        // for all points set up ref
        for(int i = 0; i < travelPoints.Length; i++)
        {
            travelPoints[i].SetupPoint(this);
        }
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
        transform.position += (travelDirection * moveSpeed) * Time.deltaTime;
    }

    //change destination
    public void ChangeDestination()
    {
        currentIndex++;
        //if the index is equal to length of array, reset
        if(currentIndex >= travelPoints.Length)
        {
            currentIndex = 0;
        }
        currentDestination = travelPoints[currentIndex].transform.position;
        //change the travel direction
        travelDirection = currentDestination - transform.position;
    }
}
