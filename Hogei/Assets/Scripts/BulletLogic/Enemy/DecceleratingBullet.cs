using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecceleratingBullet : BulletBehavior {

    [Header("Speed")]
    [Tooltip("Start speed of bullet")]
    public float startSpeed = 50.0f;
    [Tooltip("Acceleration of bullet")]
    public float decelSpeed = 1.0f;
    [Tooltip("Max speed")]
    public float minSpeed = 10.0f;

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
            currentSpeed = Mathf.Clamp(currentSpeed - decelSpeed * Time.deltaTime, minSpeed, startSpeed);
            myRigid.velocity = transform.forward * currentSpeed;
        }
    }

    //set up func
    public void SetupVars(float start, float accel, float min)
    {
        isActive = true;
        startSpeed = start;
        decelSpeed = accel;
        minSpeed = min;
        //bulletFireSound.Play();
    }
}
