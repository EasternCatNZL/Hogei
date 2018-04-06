using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckArrowShot : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 10.0f;
    [Tooltip("Bullet speed side")]
    public float bulletSpeedSide = 5.0f;
    [Tooltip("Number of bullets per attack")]
    public int numBulletsPerAttack = 5;
    [Tooltip("Number of bullets per side shot")]
    public int numBulletsSide = 5;

    [Header("Transform refs")]
    public Transform mainDuck;
    public Transform aiming;
    public Transform leftDuck;
    public Transform rightDuck;

    [Header("Timing vars")]
    [Tooltip("Time between shots")]
    public float timeBetweenShots = 0.1f;
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 4.0f;
    [Tooltip("Time between side cannon shots")]
    public float timeBetweenSideCannon = 1.0f;
    //[Tooltip("Setup time")]
    //public float setupTime = 0.1f;

    [Header("Distance vars")]
    [Tooltip("Distance between shots")]
    public float distanceBetweenBullets = 0.5f;
    //[Tooltip("Distance to set up")]
    //public float setupDistance = 0.2f;

    [Header("Angle vars")]
    [Tooltip("Base angle")]
    public float baseAngle = 45.0f;
    [Tooltip("Angle variance")]
    public float angleVariance = 1.0f;
    [Tooltip("Angle change between shots for side shots")]
    public float angleChange = 4.0f;

    [Header("Tags")]
    public string playerTag = "Player";

    [Header("Control vars")]
    public bool isUsing = false; //checks if this attack is currently being used
    private bool isAttacking = false; //checks if currently attacking
    private bool useLeft = true; //checks if to use left side
    private bool useRight = false; //check if to use right side

    private int currentShotIndex = 0; //the index of current shot

    private float timeLastShot = 0.0f; //time of last bullet
    private float timeLastAttack = 0.0f; //time last attack began
    private float timeLastSideAttack = 0.0f; //time side last attack

    private Vector3 directionToShoot = Vector3.zero;

    private GameObject player; //player ref

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isUsing)
        {
            MainShot();
            SideShot();
        }
	}

    //Main shot logic
    private void MainShot()
    {
        if (isAttacking)
        {
            if (Time.time >= timeLastShot + timeBetweenShots)
            {
                AttackLogic();
            }
        }
        else
        {
            if (Time.time >= timeLastAttack + timeBetweenAttacks)
            {
                BeginAttack();
            }
        }
    }

    //Side shot logic
    private void SideShot()
    {
        if(Time.time > timeLastSideAttack + timeBetweenSideCannon)
        {
            if (useLeft)
            {
                LeftCannon();
            }
            else if (useRight)
            {
                RightCannon();
            }
        }
        
    }

    //Begin attack logic
    private void BeginAttack()
    {
        //set is attacking to true
        isAttacking = true;
        //set timing
        timeLastAttack = Time.time;
        //if player object is not yet refed
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
        }
        //get direction to shoot
        //directionToShoot = player.transform.position - mainDuck.position;
        aiming.LookAt(player.transform.position);
    }

    //Attack logic
    private void AttackLogic()
    {
        //set timing
        timeLastShot = Time.time;
        //get spawn location using distance on right vector
        Vector3 spawnLocation = aiming.position + (aiming.right * (distanceBetweenBullets * currentShotIndex));
        //create a bullet
        GameObject bulletClone = Instantiate(bulletObject, spawnLocation, aiming.rotation);
        //set up bullet vars
        bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        //if current index greater than 0, create a second bullet
        if(currentShotIndex > 0)
        {
            //get spawn location using distance on -right vector
            spawnLocation = aiming.position - (aiming.right * (distanceBetweenBullets * currentShotIndex));
            //create a bullet
            GameObject bulletTwo = Instantiate(bulletObject, spawnLocation, aiming.rotation);
            //set up bullet vars
            bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }
        //increment index
        currentShotIndex++;
        //if index reached shots required, stop attacking and reset values
        if (currentShotIndex >= numBulletsPerAttack)
        {
            isAttacking = false;
            currentShotIndex = 0;
        }
    }

    //Side shot logic
    private void LeftCannon()
    {
        //set timing
        timeLastSideAttack = Time.time;
        //get base angle
        float angle = -baseAngle + Random.Range(-angleVariance, angleVariance);
        //for the number of bullets to shoot
        for(int i = 0; i < numBulletsSide; i++)
        {
            //create a shot from left cannon using angle * current index
            GameObject bulletClone = Instantiate(bulletObject, leftDuck.position, Quaternion.Euler(0.0f, angle + (angleChange * i), 0.0f));
            //set up the bullet
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedSide);
            //if not center bullet, create a second
            if (i > 0)
            {
                //create second bullet
                GameObject bulletTwo = Instantiate(bulletObject, leftDuck.position, Quaternion.Euler(0.0f, angle - (angleChange * i), 0.0f));
                //set up bullet
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedSide);
            }
        }
        //swap sides for next
        useLeft = false;
        useRight = true;
    }

    private void RightCannon()
    {
        //set timing
        timeLastSideAttack = Time.time;
        //get base angle
        float angle = baseAngle + Random.Range(-angleVariance, angleVariance);
        //for the number of bullets to shoot
        for (int i = 0; i < numBulletsSide; i++)
        {
            //create a shot from left cannon using angle * current index
            GameObject bulletClone = Instantiate(bulletObject, rightDuck.position, Quaternion.Euler(0.0f, angle + (angleChange * i), 0.0f));
            //set up the bullet
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedSide);
            //if not center bullet, create a second
            if (i > 0)
            {
                //create second bullet
                GameObject bulletTwo = Instantiate(bulletObject, rightDuck.position, Quaternion.Euler(0.0f, angle - (angleChange * i), 0.0f));
                //set up bullet
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedSide);
            }
        }
        //swap sides for next
        useLeft = true;
        useRight = false;
    }
}
