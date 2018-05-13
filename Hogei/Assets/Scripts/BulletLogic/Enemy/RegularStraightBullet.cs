using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularStraightBullet : BulletBehavior {

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
        
    }

    //set up func
    public void SetupVars(float speed)
    {
        isActive = true;
        travelSpeed = speed;
        //bulletFireSound.Play();
    }

    //Pause events
    protected override void OnPause()
    {
        pauseStartTime += Time.time;
        isActive = false;
    }

    protected override void OnUnpause()
    {
        isActive = true;
        pauseEndTime += Time.time;
    }
}
