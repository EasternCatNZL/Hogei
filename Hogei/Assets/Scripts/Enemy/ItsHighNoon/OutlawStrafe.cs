using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawStrafe : MonoBehaviour {

    [Header("Movement vars")]
    [Tooltip("Movement speed of the object")]
    public float moveSpeed = 5.0f;
    [Tooltip("Travel points")]
    public OutlawCheckPointHandler[] travelPoints = new OutlawCheckPointHandler[0];

    //script ref
    OutlawBehaviour outlaw;

    //control refs
    public bool isMoving = false; //check if object is moving

    private int currentIndex = 0; //the current index of the array

    private Vector3 currentDestination = Vector3.zero;
    private Vector3 travelDirection = Vector3.zero;

    Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        outlaw = GetComponent<OutlawBehaviour>();
        myRigid = GetComponent<Rigidbody>();
        SetupPointRefs();
        //debug
        currentDestination = travelPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (outlaw.isActive)
        {
            if (isMoving)
            {
                MoveBetweenPoints();
            }
            else
            {
                myRigid.velocity = Vector3.zero;
            }
        }

    }

    //Set up refs for all points
    private void SetupPointRefs()
    {
        // for all points set up ref
        for (int i = 0; i < travelPoints.Length; i++)
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
        if (currentIndex >= travelPoints.Length)
        {
            currentIndex = 0;
        }
        currentDestination = travelPoints[currentIndex].transform.position;
        //change the travel direction
        travelDirection = currentDestination - transform.position;
    }
}
