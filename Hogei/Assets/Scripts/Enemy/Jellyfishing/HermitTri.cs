using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitTri : HermitBase {


    [Tooltip("Number of bullets to shoot in burst")]
    public int numBurstFire = 3;

    [Header("Split vars")]
    [Tooltip("Split times")]
    public int splitTimes = 2;
    [Tooltip("Split into")]
    public int splitInto = 3;

    [Header("Angle control")]
    [Tooltip("Angle between bullets")]
    public float angleBetweenBullets = 60.0f;

    [Header("Attack vars")]
    [Tooltip("Time between shoots")]
    public float timeBetweenShots = 0.1f;

    private float randomAngle = 0.0f; //angle to be used in a series of shots

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hermit.isActive)
        {
            if (!isAttacking)
            {
                if (Time.time > lastAttackTime + timeBetweenAttacks)
                {
                    BeginAttacking();
                }
            }
            else
            {
                if (Time.time > lastShotTime + timeBetweenShots)
                {
                    AttackLogic();
                }
            }
        }

    }

    //Begin attack sequence
    private void BeginAttacking()
    {
        //set timing
        lastAttackTime = Time.time;
        //set the random angle for this round
        randomAngle = Random.Range(0.0f, 360.0f);
        //set is attacking to true
        isAttacking = true;
    }

    //Attack logic
    private void AttackLogic()
    {
        //set timing
        lastShotTime = Time.time;
        //for angles until 360 degrees
        for (float i = 0; i < 360; i += angleBetweenBullets)
        {
            //create a bullet facing random angle + current incrementation
            GameObject bulletClone = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, randomAngle + i, 0.0f));
            //setup vars
            bulletClone.GetComponent<SplittingBullet>().SetupVars(bulletSpeed, splitTimes, splitInto, lifeTime);
        }
        //increment currentShot
        currentShot++;
        //if current shot has reached specified limit
        if(currentShot >= numBurstFire)
        {
            //reset
            currentShot = 0;
            //stop attacking 
            isAttacking = false;
        }
    }
}
