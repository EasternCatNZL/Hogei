using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawStrafe : MonoBehaviour {

    public bool EnableStrafe = false;
    [Header("Movement vars")]
    [Tooltip("Movement speed of the object")]
    public float moveSpeed = 5.0f;
    [Tooltip("Distance to move")]
    public float moveDistance = 5.0f;
    [Tooltip("Movement direction")]
    [Range(-1, 1)]
    public int direction = 1;
    //[Tooltip("Travel points")]
    //public OutlawCheckPointHandler[] travelPoints = new OutlawCheckPointHandler[0];

    [Header("Timing check")]
    [Tooltip("Time before distance check can occur again")]
    public float checkDelay = 1.0f;

    [Header("Animation tags")]
    public string leftStep = "IsLeftStep";
    public string rightStep = "IsRightStep";

    //script ref
    OutlawBehaviour outlaw;

    //animator
    private Animator anim; //anim attached to this object

    //control refs
    public bool isMoving = false; //check if object is moving

    //private int currentIndex = 0; //the current index of the array

    private float lastCheckTime = 0.0f; //last time that check occured

    private Vector3 startPos = Vector3.zero;
    //Debugging value for custom inspector button
    public bool CanStrafe = false;

    //private Vector3 currentDestination = Vector3.zero;
    //private Vector3 travelDirection = Vector3.zero;

    Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        outlaw = GetComponent<OutlawBehaviour>();
        myRigid = GetComponent<Rigidbody>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
        //SetupPointRefs();
        //debug
        //currentDestination = travelPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableStrafe)
        {
            if (outlaw.isActive || CanStrafe)
            {
                Move();
            }
        }
    }

    //move back and forth
    private void Move()
    {
        //if timing
        if(Time.time > lastCheckTime + checkDelay)
        {
            //if max distance has been covered
            if (Vector3.Distance(startPos, transform.position) > moveDistance)
            {
                //change directions
                direction *= -1;
                //set timing
                lastCheckTime = Time.time;
            }
        }
        //move
        myRigid.velocity = (transform.right * direction) * moveSpeed;

        //handle animation
        if(direction == -1)
        {
            anim.SetBool(leftStep, true);
            anim.SetBool(rightStep, false);
        }
        else if (direction == 1)
        {
            anim.SetBool(leftStep, false);
            anim.SetBool(rightStep, true);
        }
    }

    ////Set up refs for all points
    //private void SetupPointRefs()
    //{
    //    // for all points set up ref
    //    for (int i = 0; i < travelPoints.Length; i++)
    //    {
    //        travelPoints[i].SetupPoint(this);
    //    }
    //}

    ////Move between points
    //private void MoveBetweenPoints()
    //{
    //    //get vector towards target
    //    Vector3 desireVelocity = currentDestination - transform.position;
    //    float distance = desireVelocity.magnitude;
    //    desireVelocity = Vector3.Normalize(desireVelocity) * moveSpeed;
    //    //get steering force
    //    Vector3 steeringForce = desireVelocity - myRigid.velocity;
    //    //adjust velocity
    //    myRigid.velocity = Vector3.ClampMagnitude(myRigid.velocity + (steeringForce * Time.deltaTime), moveSpeed);
    //}

    ////change destination
    //public void ChangeDestination()
    //{
    //    currentIndex++;
    //    //if the index is equal to length of array, reset
    //    if (currentIndex >= travelPoints.Length)
    //    {
    //        currentIndex = 0;
    //    }
    //    currentDestination = travelPoints[currentIndex].transform.position;
    //    //change the travel direction
    //    travelDirection = currentDestination - transform.position;
    //}
}
