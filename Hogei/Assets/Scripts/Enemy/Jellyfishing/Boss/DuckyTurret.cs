using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckyTurret : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of shots per wave")]
    public int numShotsWave = 15;
    [Tooltip("Number of bullets per shot")]
    public int numBulletsShot = 2;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 10.0f;

    [Header("Timing vars")]
    [Tooltip("Time between shots")]
    public float timeBetweenShots = 0.1f;

    [Header("Angle control")]
    [Tooltip("Angle between bullets")]
    public float angleBetweenBullets = 4.0f;
    [Tooltip("Angle between sprays")]
    public float angleBetweenSprays = 2.0f;
    [Tooltip("Starting angle")]
    public float startingAngle = 20.0f;
    [Tooltip("Angle variance")]
    public float angleVariance = 1.5f;
    [Tooltip("Direction")]
    [Range(-1, 1)]
    public int direction = 1;

    //control vars
    private int currentShot = 0; //the current shot

    private float currentAngle = 0.0f; //the current angle
    private float thisSprayAngleAlt = 0.0f; //the random angle alt used for this spray
    private float lastShotTime = 0.0f; //the time of last shot
    private float attackEndTime = 0.0f; //time attack ended

    public bool isShooting = false; //checks if currently attacking

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isShooting)
        {
            if(Time.time > lastShotTime + timeBetweenShots)
            {
                TurretSpray();
            }
        }
	}

    //Turret spray
    private void TurretSpray()
    {
        //for all bullets in a spray
        for (int i = 0; i < numBulletsShot; i++)
        {
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, (currentAngle + (angleBetweenBullets * i)) * direction, 0.0f));
            //set up bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }
        

        //increment the current shot
        currentShot++;
        //if current shot = num shots in wave, reset
        if(currentShot >= numShotsWave)
        {
            //is attacking to false
            isShooting = false;
            //reset current shot
            currentShot = 0;
            //reset current angle
            currentAngle = 0.0f;
            //set attack end time
            attackEndTime = Time.time;
        }
        else
        {
            //set timing
            lastShotTime = Time.time;
            //change angle for next shot
            currentAngle += angleBetweenSprays;
        }
    }

    //Attack sequence start logic
    public void FireTurrets()
    {
        //set is attacking to true
        isShooting = true;
        //set current angle to start angle with random alter
        currentAngle = startingAngle + Random.Range(-angleVariance, angleVariance);
    }
}
