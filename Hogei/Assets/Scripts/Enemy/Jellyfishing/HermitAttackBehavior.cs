using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitAttackBehavior : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("The bullet object that is fired")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 10.0f;
    [Tooltip("Number of bubbles")]
    public int numberOfBullets = 3;

    [Header("Attack vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 1.0f;
    [Tooltip("Time last ready")]
    public float timeTillReady = 1.0f;
    [Tooltip("Time between bubbles")]
    public float timeBetweenBubbles = 0.1f;

    [Header("Angle control")]
    [Tooltip("Angle shot can be fired out at")]
    public float angleOut = 30.0f;

    //script refs
    public HermitMoveBehavior hermit;

    //control vars
    public bool isAttacking = false; //checks if attacked
    public bool isReadying = false; //checks if currently getting ready
    private bool canShoot = false;

    private int currentShot = 0; //the current index of shots

    private float lastAttackTime = 0.0f; //the time last attack occured
    private float lastShotTime = 0.0f; //the time last bubble was shot
    private float lastReadyUpTime = 0.0f; //the time last ready for attack

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (hermit.isActive)
        {
            AttackBehavior();
        }
	}

    //Attack behavior logic
    private void AttackBehavior()
    {
        if (isReadying)
        {
            if (Time.time > timeTillReady + lastReadyUpTime)
            {
                AttackSequence();
            }
        }
        else if (isAttacking)
        {
            if (canShoot)
            {
                if (Time.time > lastShotTime + timeBetweenBubbles)
                {
                    FireBubble();
                }
            }
            if (Time.time > lastAttackTime + timeBetweenAttacks)
            {
                ContinueMovement();
            }
        }
    }

    //Shoot out bubble
    private void FireBubble()
    {
        //get a random angle
        float randomAngle = Random.Range(-angleOut, angleOut);
        //create a bullet
        GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
        //set the rotation
        Quaternion newRotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + randomAngle, 0.0f);
        bulletClone.transform.rotation = newRotation;
        bulletClone.GetComponent<Rigidbody>().velocity = bulletClone.transform.forward * bulletSpeed;

        //increase the index of current shot
        currentShot++;
        print("Shot: " + currentShot);

        //if current shot = num of bullets
        if(currentShot >= numberOfBullets)
        {
            //reset currentshot to 0
            currentShot = 0;
            //set can shoot to false
            canShoot = false;
        }
        //otherwise set last shot time to now
        else
        {
            lastShotTime = Time.time;
            
        }
    }

    //Prepare attack
    private void AttackSequence()
    {
        //set timing
        lastAttackTime = Time.time;
        //turn movement off
        hermit.isMoving = false;
        ////attack
        //FireBubble();
        //set attacking to true
        isAttacking = true;
        isReadying = false;
        canShoot = true;
    }

    //Continue movement
    private void ContinueMovement()
    {
        //set timing
        lastReadyUpTime = Time.time;
        //turn movement on
        hermit.isMoving = true;
        //set is readying to true
        isReadying = true;
        isAttacking = false;
    }
}
