using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishSpinBehavior : EnemyBehavior {

    [Header("Spin vars")]
    [Tooltip("The max speed that starfish spins at")]
    public float maxSpinSpeed = 720.0f;
    [Tooltip("Spin speed change rate")]
    public float spinSpeedChangeRate = 60.0f;

    [Header("Shot points")]
    [Tooltip("Points where to shoot bullets from")]
    public Transform[] shotPointArray = new Transform[0];

    [Header("Bullet vars")]
    [Tooltip("The bullet object")]
    public GameObject bulletObject;
    [Tooltip("The speed of the bullet")]
    public float bulletSpeed = 5.0f;

    [Header("Timing vars")]
    [Tooltip("Time between shots")]
    public float timeBetweenShots = 0.1f;

    [Header("Timing vars")]
    [Tooltip("The amount of time max speed is held")]
    public float maxSpeedHoldTime = 3.0f;

    //control vars
    private bool isHoldingMaxSpeed = false; //checks to see if max speed is currently being held

    private int currentDirection = 1; //the direction the starfish is spinning

    private float currentSpeed = 0.0f; //the current speed of the spin
    private float maxSpeedReachedTime = 0.0f; //the time max speed was reached
    private float lastShotTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            Spin();
            PointShot();
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

    //point shot behavior
    private void PointShot()
    {
        //if timing reached
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //for all points
            for (int i = 0; i < shotPointArray.Length; i++)
            {

                //create a clone of the bullet
                GameObject bulletClone = Instantiate(bulletObject, shotPointArray[i].transform.position, shotPointArray[i].transform.rotation);
                //set the speed of the bullet
                bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
                //set timing to now
                lastShotTime = Time.time;

            }
        }
    }
}
