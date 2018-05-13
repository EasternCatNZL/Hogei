using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingBullet : BulletBehavior {

    [Header("Speed")]
    [Tooltip("Start speed of bullet")]
    public float startSpeed = 1.0f;
    [Tooltip("Acceleration of bullet")]
    public float accelSpeed = 1.0f;
    [Tooltip("Max speed")]
    public float maxSpeed = 10.0f;

    //control vars
    private float currentSpeed = 0.0f;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
        currentSpeed = startSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
            currentSpeed = Mathf.Clamp(currentSpeed + accelSpeed * Time.deltaTime, startSpeed, maxSpeed);
            myRigid.velocity = transform.forward * currentSpeed;
        }
	}

    //set up func
    public void SetupVars(float start, float accel, float max)
    {
        isActive = true;
        startSpeed = start;
        accelSpeed = accel;
        maxSpeed = max;
        //bulletFireSound.Play();
    }
}
