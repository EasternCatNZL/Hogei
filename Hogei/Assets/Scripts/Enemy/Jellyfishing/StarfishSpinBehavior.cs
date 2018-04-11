using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishSpinBehavior : EnemyBehavior {

    [Header("Spin vars")]
    [Tooltip("The max speed that starfish spins at")]
    public float maxSpinSpeed = 5.0f;
    [Tooltip("Spin speed change rate")]
    public float spinSpeedChangeRate = 0.5f;

    [Header("Timing vars")]
    [Tooltip("The amount of time max speed is held")]
    public float maxSpeedHoldTime = 3.0f;

    //control vars
    private bool isHoldingMaxSpeed = false; //checks to see if max speed is currently being held

    private int currentDirection = 1; //the direction the starfish is spinning

    private float currentSpeed = 0.0f; //the current speed of the spin
    private float maxSpeedReachedTime = 0.0f; //the time max speed was reached


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            Spin();
        }
	}

    //spin logic
    private void Spin()
    {
        //check if curretnly holding max speed
        if (isHoldingMaxSpeed)
        {
            //if timing has been reached
            if(Time.time > maxSpeedReachedTime + maxSpeedHoldTime)
            {
                //change the direction
                ChangeDirection();
                //set is holding to false
                isHoldingMaxSpeed = false;
            }
        }
        //else alter speed based on rate against time
        else
        {
            //get the current speed
            currentSpeed += (spinSpeedChangeRate * currentDirection) * Time.deltaTime;
            //check if reached max speed
            CheckMaxSpeed();
        }

        //set the rotation of the object
        transform.Rotate(transform.up, currentSpeed * Time.deltaTime);

        //print("Current speed = " + currentSpeed);
    }

    //check if max speed reached
    private void CheckMaxSpeed()
    {
        if (currentDirection == 1)
        {
            if (currentSpeed >= maxSpinSpeed)
            {
                currentSpeed = maxSpinSpeed;
                //hold max speed
                isHoldingMaxSpeed = true;
                //set timing
                maxSpeedReachedTime = Time.time;
            }
        }
        else if (currentDirection == -1)
        {
            if (currentSpeed <= -maxSpinSpeed)
            {
                currentSpeed = -maxSpinSpeed;
                //hold max speed
                isHoldingMaxSpeed = true;
                //set timing
                maxSpeedReachedTime = Time.time;
            }
        }
    }

    //check if current speed has reached max speed based on current direction
    private void ChangeDirection()
    {
        if (currentDirection == 1)
        {
            currentDirection *= -1;
        }
        else if (currentDirection == -1)
        {
            currentDirection *= -1;
        }
    }
}
