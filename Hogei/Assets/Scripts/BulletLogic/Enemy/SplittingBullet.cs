using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingBullet : BulletBehavior {

    [Header("Split vars")]
    [Tooltip("Number to split into")]
    public int numToSplit = 3;
    [Tooltip("Remaining splits")]
    public int remainingSplits = 2;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                //if can still split
                if(remainingSplits > 0)
                {
                    Split();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }

    //split the bullet at end of life time if splits remain
    private void Split()
    {
        //get the angle to split at
        float splitAngle = 360.0f / 3.0f;
        //get a random starting angle
        float randomAngle = Random.Range(0.0f, 360.0f);
        //for the number of bullets
        for (int i = 0; i < numToSplit; i++)
        {
            //create a bullet
            GameObject bulletClone = Instantiate(gameObject, transform.position, Quaternion.Euler(0.0f, splitAngle * i + transform.rotation.eulerAngles.y /*+ randomAngle*/, 0.0f));
            //set up the bullets
            bulletClone.GetComponent<SplittingBullet>().SetupVars(travelSpeed, remainingSplits - 1, numToSplit);
        }
        //destroy self after
        Destroy(gameObject);
    }

    //setup the bullet
    public void SetupVars(float speed, int splitTimes, int numSplit)
    {
        travelSpeed = speed;
        remainingSplits = splitTimes;
        numToSplit = numSplit;
    }

    public void SetupVars(float speed, int splitTimes, int numSplit, float life)
    {
        travelSpeed = speed;
        remainingSplits = splitTimes;
        numToSplit = numSplit;
        lifeTime = life;
    }
}
