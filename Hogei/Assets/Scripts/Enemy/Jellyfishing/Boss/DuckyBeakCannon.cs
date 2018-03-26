using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DuckyBeakCannon : MonoBehaviour {

    [Header("Laser vars")]
    [Tooltip("Laser length")]
    public float laserLength = 20.0f;
    //[Tooltip("Turn speed")]
    //public float turnSpeed = 20.0f;

    [Header("Timing vars")]
    [Tooltip("Amount of time to turn head")]
    public float turnTime = 3.0f;
    [Tooltip("Start delay before turning begins")]
    public float startDelay = 0.5f;

    [Header("Rotation control")]
    [Tooltip("Starting point of the rotation")]
    [Range(0.0f, 360.0f)]
    public float rotationStart = 70.0f;
    [Tooltip("End point of the rotation")]
    [Range(0.0f, 360.0f)]
    public float rotationEnd = 20.0f;
    [Tooltip("Direction")]
    [Range(-1, 1)]
    public int direction = 1;

    //[Header("Parent object")]
    //[Tooltip("Parent object ref, for easy rotation refs")]
    //public GameObject parent;

    //control refs
    private bool isAttacking = false; //check if currently attacking
    private bool isTurning = false; //check if head is currently turning

    private float laserStartTime = 0.0f; //time laser started

    private float thisStartRot = 0.0f; //the start rotation for this round
    private float thisEndRot = 0.0f; //the end rotation for this round

    private Vector3[] lineVectors = new Vector3[2];

    private LineRenderer line;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
	}
	
	// Update is called once per frame
	void Update () {
        //DrawLineToPoint();

        if (isAttacking)
        {
            Attack();
        }
	}

    //draw line
    private void DrawLineToPoint()
    {
        //get positions
        lineVectors[0] = transform.position;
        lineVectors[1] = transform.position + (transform.forward * laserLength);
        //draw
        line.SetPositions(lineVectors);
    }

    //start laser
    public void StartLaser()
    {
        //set the start and end rots of this round
        thisStartRot = rotationStart * direction;
        thisEndRot = rotationEnd * direction;
        //change rotation to start rotation
        transform.rotation = Quaternion.Euler(0.0f, thisStartRot, 0.0f);
        //set timing
        laserStartTime = Time.time;
        //set is attacking to true
        isAttacking = true;
    }

    //attack logic
    private void Attack()
    {
        FireLaser();
        if (!isTurning && Time.time > laserStartTime + startDelay)
        {
            TurnHead();
        }
        if (Time.time > laserStartTime + turnTime)
        {
            EndLaser();
        }
    }

    //end laser
    private void EndLaser()
    {
        //set is attacking to false
        isAttacking = false;
        isTurning = false;
        //reset rotation
        transform.rotation = Quaternion.identity;
        //change directions for next round
        direction *= -1;
    }

    //fire laser
    private void FireLaser()
    {
        //for now
        DrawLineToPoint();
    }

    //Turn head
    private void TurnHead()
    {
        //turn head
        transform.DORotate(new Vector3(0.0f, thisEndRot, 0.0f), turnTime);
        //set is turning to true
        isTurning = true;
    }
}
