using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitLong : HermitBase {

    //[Header("Spray vars")]
    //[Tooltip("Radius of bullets")]
    //public int numBullets = 4;
    //[Tooltip("Setup distance")]
    //public float setupDistance = 3.0f;
    //[Tooltip("First layer speed")]
    //public float firstLayerBulletSpeed = 1.0f;
    //[Tooltip("Layer speed increment value")]
    //public float layerSpeedIncrementValue = 0.5f;
    [Tooltip("Time between steps")]
    public float timeBetweenSteps = 1.0f;

    [Header("Movement angles")]
    [Tooltip("The angle for bullets to move in after launch")]
    public float moveAngle = 30.0f;

    [Header("Timing vars")]
    [Tooltip("Time needed to setup")]
    public float timeToSetup = 1.0f;
    [Tooltip("Delay before movement begins")]
    public float startDelay = 0.5f;

    //control vars
    private bool firstShot = true; //checks if is first shot

    public int currentMiss = 0; //the shot to miss this go-around
    private int lastMiss = 0; //the last shot that was skipped

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hermit.isActive)
        {
            if (Time.time > lastAttackTime + timeBetweenAttacks)
            {
                AttackLogic();
            }
        }

    }

    /*
    //Bullet block maze
    private void AttackLogic()
    {
        //set timing
        lastAttackTime = Time.time;
        //if first shot get random shot
        if (firstShot)
        {
            currentMiss = Random.Range(-numBulletsWide, numBulletsWide);
            //change first shot to false
            firstShot = false;
        }
        //else get random +- 1 from last
        else
        {
            currentMiss = Random.Range(lastMiss - 1, lastMiss + 2);
            //if exceeds width, just set back to prev
            if (currentMiss < -numBulletsWide + 1)
            {
                currentMiss += 2;
            }
            else if (currentMiss > numBulletsWide - 1)
            {
                currentMiss -= 2;
            }
        }
        //for all bullets in row
        for (int i = -numBulletsWide; i <= numBulletsWide; i++)
        {
            //if i = miss, skip
            if (i != currentMiss && i - 1 != currentMiss && i + 1 != currentMiss)
            {
                //get the setup location
                Vector3 setupPos = transform.position + (transform.right * setupDistance * i);
                //create a clone of bullet
                GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
                //setup the bullet
                bulletClone.GetComponent<SetupBullet>().SetupVars(setupPos, timeToSetup, startDelay, bulletSpeed, lifeTime);
            }
        }
        //set last miss to current miss
        lastMiss = currentMiss;
    }
    */

    //Attack logic
    private void AttackLogic()
    {
        //set timing
        lastAttackTime = Time.time;
        //make list of angles
        List<float> angleListOne = new List<float>();
        angleListOne.Add(moveAngle);
        angleListOne.Add(-moveAngle);
        //make first bullet
        GameObject bulletOne = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, moveAngle, 0.0f));
        bulletOne.GetComponent<RepeatStepBullet>().SetupVars(bulletSpeed, angleListOne, timeBetweenSteps, lifeTime);

        //make list of angles
        List<float> angleListTwo = new List<float>();
        angleListTwo.Add(-moveAngle);
        angleListTwo.Add(moveAngle);
        //make first bullet
        GameObject bulletTwo = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, -moveAngle, 0.0f));
        bulletTwo.GetComponent<RepeatStepBullet>().SetupVars(bulletSpeed, angleListTwo, timeBetweenSteps, lifeTime);

        /*
        //for the number of bullets
        for (int i = 0; i < numBullets; i++)
        {
            //create a bullet in distance right of self
            Vector3 spawnPos = transform.position + transform.right * setupDistance;
            GameObject bulletOne = Instantiate(bulletObject, spawnPos, transform.rotation);
            //setup the vars
            bulletOne.GetComponent<RegularStraightBullet>().SetupVars(firstLayerBulletSpeed + (layerSpeedIncrementValue * i));

            //create the second bullet
            spawnPos = transform.position - transform.right * setupDistance;
            GameObject bulletTwo = Instantiate(bulletObject, spawnPos, transform.rotation);
            //setup the vars
            bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(firstLayerBulletSpeed + (layerSpeedIncrementValue * i));
        }
        */
    }
}
