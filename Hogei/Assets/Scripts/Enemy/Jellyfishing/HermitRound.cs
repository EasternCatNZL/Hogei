using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitRound : HermitBase {

    [Header("Angle control")]
    [Tooltip("Angle between bullets")]
    public float angleBetweenBullets = 60.0f;
    [Tooltip("Rotation amount of bullet")]
    public float bulletRotationSpeed = 5.0f;
    [Tooltip("Starting direction")]
    [Range(-1, 1)]
    public int startingRotation = 1;
    [Tooltip("Change rotation direction")]
    public bool changeDirection = true;

    //control vars
    private int rotationDireciton; //the rotation direction


    // Use this for initialization
    void Start () {
        rotationDireciton = startingRotation;
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

    //Attack logic
    private void AttackLogic()
    {
        //set timing
        lastAttackTime = Time.time;
        //get a random starting angle
        float randomAngle = Random.Range(0.0f, 360.0f);
        //for angles until 360 degrees
        for(float i = 0; i < 360; i += angleBetweenBullets)
        {
            //create a bullet facing random angle + current incrementation
            GameObject bulletClone = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, randomAngle + i, 0.0f));
            //setup vars
            bulletClone.GetComponent<TurningBullet>().SetupVars(bulletRotationSpeed, rotationDireciton, bulletSpeed, lifeTime);
        }
        //if direction is changing
        if (changeDirection)
        {
            rotationDireciton *= -1;
        }
    }
}
