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
    private bool isMoving = false; //check if object is moving

    private int currentIndex = 0; //the current index of the array

    private Vector3 currentDestination = Vector3.zero;
    private Vector3 travelDirection = Vector3.zero;

    private Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();
        SetupPointRefs();
        //debug
        currentDestination = travelPoints[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (isMoving)
            {
                MoveBetweenPoints();
            }
        }
        
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
        //get vector towards target
        Vector3 desireVelocity = currentDestination - transform.position;
        float distance = desireVelocity.magnitude;
        desireVelocity = Vector3.Normalize(desireVelocity) * moveSpeed;
        //get steering force
        Vector3 steeringForce = desireVelocity - myRigid.velocity;
        //adjust velocity
        myRigid.velocity = Vector3.ClampMagnitude(myRigid.velocity + (steeringForce * Time.deltaTime), moveSpeed);
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
