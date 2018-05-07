using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCucumberBehavior : EnemyBehavior {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Header("Spray A")]
    [Tooltip("Bullet speed A")]
    public float bulletSpeedA = 5.0f;
    //[Tooltip("Number of shots in spray A")]
    //public int numShotsSprayA = 1;
    [Tooltip("Time between shots")]
    public float timeBetweenShotsA = 0.1f;
    [Tooltip("Angle change between shots")]
    public float angleChangeShotsA = 5.0f;
    [Tooltip("Direction")]
    [Range(-1, 1)]
    public int directionA = 1;

    [Header("Spray B")]
    [Tooltip("Bullet speed b")]
    public float bulletSpeedB = 10.0f;
    [Tooltip("Number of shots in spray b")]
    public int numShotsSprayB = 2;
    [Tooltip("Time between shots")]
    public float timeBetweenShotsB = 0.2f;
    [Tooltip("Angle change between bullets")]
    public float angleChangeBetweenBulletsB = 4.0f;
    [Tooltip("Angle change between shots")]
    public float angleChangeShotsB = 10.0f;
    [Tooltip("Direction")]
    [Range(-1, 1)]
    public int directionB = -1;

    [Header("Init vars")]
    [Tooltip("Starting angle of A")]
    public float startingAngleA = 0.0f;
    [Tooltip("Starting angle of B")]
    public float startingAngleB = 20.0f;
    [Tooltip("Bullet Spawn Height")]
    public float bulletSpawnHeight = 1f;

    //control vars spray a
    private float timeLastShotA = 0.0f; //time spray a last fired
    private float currentAngleA = 0.0f; //the current angle of spray a

    //control vars spray b
    private float timeLastShotB = 0.0f; //time spray b last fired
    private float currentAngleB = 0.0f; //the current angle of spray b

	// Use this for initialization
	void Start () {
        currentAngleA = startingAngleA;
        currentAngleB = startingAngleB;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (Time.time > timeLastShotA + timeBetweenShotsA)
            {
                SprayA();
            }
            if (Time.time > timeLastShotB + timeBetweenShotsB)
            {
                SprayB();
            }
        }

	}

    //Spray a logic
    private void SprayA()
    {
        //Create Bullet Spawn Position
        Vector3 bulletSpawn = new Vector3(transform.position.x, transform.position.y + bulletSpawnHeight, transform.position.z);

        //set timing
        timeLastShotA = Time.time;

        //create a bullet
        GameObject bulletClone = Instantiate(bulletObject, bulletSpawn, Quaternion.Euler(0.0f, currentAngleA, 0.0f));

        //set the bullet variables
        bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedA);

        //create a second bullet
        GameObject bulletTwo = Instantiate(bulletObject, bulletSpawn, Quaternion.Euler(0.0f, currentAngleA + 180.0f, 0.0f));

        //set the bullet variables
        bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedA);

        //increment angle
        currentAngleA += angleChangeShotsA * directionA;
    }

    //spray b logic
    private void SprayB()
    {
        //Create Bullet Spawn Position
        Vector3 bulletSpawn = new Vector3(transform.position.x, transform.position.y + bulletSpawnHeight, transform.position.z);

        //set timing
        timeLastShotB = Time.time;

        //for all bullets
        for(int i = 0; i < numShotsSprayB; i++)
        {
            //get the angle for this bullet
            float thisRot = currentAngleB + (i * (angleChangeBetweenBulletsB * directionB));

            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, bulletSpawn, Quaternion.Euler(0.0f, thisRot, 0.0f));

            //set the bullet variables
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedB);

            //create a second bullet
            GameObject bulletTwo = Instantiate(bulletObject, bulletSpawn, Quaternion.Euler(0.0f, thisRot + 180.0f, 0.0f));

            //set the bullet variables
            bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeedB);
        }

        //increment angle
        currentAngleB += angleChangeShotsB * directionB;
    }
}
