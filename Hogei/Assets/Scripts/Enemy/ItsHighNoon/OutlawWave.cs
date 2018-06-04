using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawWave : OutlawBehaviour {

    [Header("Wave vars")]
    [Tooltip("Min num of bullets")]
    public int minNumBullets = 1;
    [Tooltip("Max num of bullets")]
    public int maxNumBullets = 5;
    [Tooltip("Angle between shots")]
    public float angleBetweenShots = 5.0f;
    [Tooltip("Wave angle offset")]
    public float angleOffset = 2.5f;

    //[Header("Animation tags")]
    //public string attackTrigger = "DoAttack";

    //control vars
    private bool isAttacking = false; //check if currently attacking

    private float timeLastRound = 0.0f; //time of last attack sequence

    //animator
    private Animator anim; //anim attached to this object

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if(isActive)
            {
                if (!isAttacking)
                {
                    if (Time.time > timeLastRound + timeBetweenRounds)
                    {
                        BeginAttack();
                    }
                }
                else
                {
                    if(Time.time > timeLastShot + timeBetweenShots)
                    {
                        AttackBehavior();
                    }
                }
            }
        }
	}

    //begin attack logic
    private void BeginAttack()
    {
        //set timing
        timeLastRound = Time.time;
        //set attacking to true
        isAttacking = true;
    }

    //attack logic
    private void AttackBehavior()
    {
        //set timing
        timeLastShot = Time.time;
        //if odd, add offset angle
        float offset = 0.0f;
        bool odd = false;
        if(currentShotInRound % 2 != 0)
        {
            offset = angleOffset;
            odd = true;
        }
        //if odd
        if (odd)
        {
            //for the bullets in current wave
            for (int i = 1; i < currentShotInRound; i++)
                {
                    //create a bullet
                    GameObject bulletOne = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (angleBetweenShots * (i - 1) + offset), 0.0f));
                    //set up vars
                    bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
                //if not center bullet, make second bullet
                //if (i > 1)
                //{
                //create second bullet
                GameObject bulletTwo = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (angleBetweenShots * (i - 1) + offset), 0.0f));
                //set up vars 
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
                //}

            }
        }
        else
        {
            //for the bullets in current wave
            for (int i = 1; i < currentShotInRound; i++)
            {
                //create a bullet
                GameObject bulletOne = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + angleBetweenShots * (i - 1) + offset, 0.0f));
                //set up vars
                bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);


                //create second bullet
                GameObject bulletTwo = Instantiate(bulletObject, transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (angleBetweenShots * (i - 1) + offset), 0.0f));
                //set up vars 
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);


            }
        }

        //fire animator
        anim.SetTrigger(attackTrigger);

        //increment current shot
        currentShotInRound++;
        //if current shot has reached max, reset
        if(currentShotInRound > maxNumBullets)
        {
            currentShotInRound = 1;
            isAttacking = false;
        }
    }
}
