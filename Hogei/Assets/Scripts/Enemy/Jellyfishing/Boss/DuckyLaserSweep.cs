using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DuckyLaserSweep : MonoBehaviour {

    [Header("Laser vars")]
    [Tooltip("Laser length")]
    public float laserLength = 20.0f;
    [Tooltip("Line renderer")]
    public LineRenderer line;

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of shots per wave")]
    public int numShotsWave = 15;
    [Tooltip("Number of bullets per shot")]
    public int numBulletsShot = 2;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 10.0f;

    [Header("Transform refs")]
    public Transform mainDuck;
    public Transform leftDuck;
    public Transform rightDuck;

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 3.0f;
    [Tooltip("Amount of time to turn head")]
    public float turnTime = 3.0f;
    [Tooltip("Start delay before turning begins")]
    public float startDelay = 0.5f;
    [Tooltip("Time between turrets")]
    public float timeBetweenTurrets = 2.0f;
    [Tooltip("Time between shots")]
    public float timeBetweenShots = 0.1f;

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

    [Header("Angle control")]
    [Tooltip("Angle between bullets")]
    public float angleBetweenBullets = 4.0f;
    [Tooltip("Angle between sprays")]
    public float angleBetweenSprays = 2.0f;
    [Tooltip("Starting angle")]
    public float startingAngle = 20.0f;
    [Tooltip("Angle variance")]
    public float angleVariance = 1.5f;

    [Header("Control vars")]
    public bool isAttacking = false; //check if currently attacking
    public bool isTurning = false; //check if head is currently turning
    public bool isShootingLeft = false; //checks if left currently attacking.
    public bool isShootingRight = false; //checks if right turret attacking

    //laser control vars
    private float laserStartTime = 0.0f; //time laser started

    private float thisStartRot = 0.0f; //the start rotation for this round
    private float thisEndRot = 0.0f; //the end rotation for this round

    private Vector3[] lineVectors = new Vector3[2];

    //turret control vars
    private int currentShotLeft = 0; //the current shot left
    private int currentShotRight = 0; //the current shot right

    private float currentAngleLeft = 0.0f; //the current angle left turret
    private float currentAngleRight = 0.0f; //the curretn angle right turret
    private float thisSprayAngleAlt = 0.0f; //the random angle alt used for this spray
    private float lastShotTimeLeft = 0.0f; //the time of left shot
    private float lastShotTimeRight = 0.0f; //the time of right shot
    private float attackEndTimeLeft = 0.0f; //time attack ended left
    private float attackEndTimeRight = 0.0f; //time attack ended right

    // Use this for initialization
    void Start () {
        line.positionCount = 2;
    }
	
	// Update is called once per frame
	void Update () {
        AttackLogic();
    }

    //attack logic in update
    private void AttackLogic()
    {
        if (Time.time > timeBetweenAttacks + laserStartTime)
        {
            StartLaser();
        }
        if (isAttacking)
        {
            LaserAttack();
        }
        if(!isShootingLeft && !isShootingRight)
        if (Time.time > attackEndTimeLeft + timeBetweenTurrets)
        {
            FireTurrets();
        }

        if (isShootingLeft)
        {
            if (Time.time > lastShotTimeLeft + timeBetweenShots)
            {
                LeftTurretSpray();
            }
        }
        if (isShootingRight)
        {
            if (Time.time > lastShotTimeRight + timeBetweenShots)
            {
                RightTurretSpray();
            }
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
        mainDuck.rotation = Quaternion.Euler(0.0f, thisStartRot, 0.0f);
        //set timing
        laserStartTime = Time.time;
        //set is attacking to true
        isAttacking = true;
    }

    //attack logic
    private void LaserAttack()
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
        mainDuck.rotation = Quaternion.identity;
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
        mainDuck.DORotate(new Vector3(0.0f, thisEndRot, 0.0f), turnTime);
        //set is turning to true
        isTurning = true;
    }

    //Left turret
    private void LeftTurretSpray()
    {
        //for all bullets in a spray
        for (int i = 0; i < numBulletsShot; i++)
        {
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, leftDuck.position, Quaternion.Euler(0.0f, currentAngleLeft + (angleBetweenBullets * i) * -1, 0.0f));
            //set up bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }

        //incremetn the current shot
        currentShotLeft++;
        //if current shot = num shots in wave, reset
        if(currentShotLeft >= numShotsWave)
        {
            //left shoot to false
            isShootingLeft = false;
            //reset current shot
            currentShotLeft = 0;
            //reset current angle
            currentAngleLeft = 0.0f;
            //set attack end time
            attackEndTimeLeft = Time.time;
        }
        else
        {
            //set timing
            lastShotTimeLeft = Time.time;
            //change angle for next shot
            currentAngleLeft -= angleBetweenSprays;
        }
        print(currentAngleLeft);
    }

    //right turret
    private void RightTurretSpray()
    {
        //for all bullets in a spray
        for (int i = 0; i < numBulletsShot; i++)
        {
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, rightDuck.position, Quaternion.Euler(0.0f, currentAngleRight + (angleBetweenBullets * i) * 1, 0.0f));
            //set up bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }

        //incremetn the current shot
        currentShotRight++;
        //if current shot = num shots in wave, reset
        if (currentShotRight >= numShotsWave)
        {
            //left shoot to false
            isShootingRight = false;
            //reset current shot
            currentShotRight = 0;
            //reset current angle
            currentAngleRight = 0.0f;
            //set attack end time
            attackEndTimeRight = Time.time;
        }
        else
        {
            //set timing
            lastShotTimeRight = Time.time;
            //change angle for next shot
            currentAngleRight += angleBetweenSprays;
        }
    }

    //Attack sequence start logic
    public void FireTurrets()
    {
        //set is attacking to true
        isShootingLeft = true;
        isShootingRight = true;
        //set current angle to start angle with random alter
        currentAngleLeft = -startingAngle + Random.Range(-angleVariance, angleVariance);
        currentAngleRight = startingAngle + Random.Range(-angleVariance, angleVariance);
    }
}
