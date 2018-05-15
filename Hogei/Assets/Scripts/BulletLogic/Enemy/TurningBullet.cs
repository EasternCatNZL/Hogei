using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningBullet : BulletBehavior {

    [Header("Rotation")]
    [Tooltip("Speed of rotation")]
    public float rotationSpeed = 5.0f;
    [Tooltip("Rotation direction")]
    [Range(-1, 1)]
    public int rotationDireciton = 1;

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
                Destroy(gameObject);
            }
            RotateMove();
        }
    }

    //rotate bullet
    private void RotateMove()
    {
        //rotate a set amount
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime * rotationDireciton);
        //move forward at speed
        myRigid.velocity = transform.forward * travelSpeed;
    }

    //set up func
    public void SetupVars(float rotSpeed, int rotDirection, float speed)
    {
        travelSpeed = speed;
        rotationSpeed = rotSpeed;
        rotationDireciton = rotDirection;
    }

    //set up func
    public void SetupVars(float rotSpeed, int rotDirection, float speed, float life)
    {
        travelSpeed = speed;
        rotationSpeed = rotSpeed;
        rotationDireciton = rotDirection;
        lifeTime = life;
    }
}
