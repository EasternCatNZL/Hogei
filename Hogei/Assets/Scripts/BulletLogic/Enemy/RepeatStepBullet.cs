using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatStepBullet : BulletBehavior {

    [Header("Steps")]
    [Tooltip("Number of steps")]
    public int numSteps = 0;
    [Tooltip("Time between step changes")]
    public float timeBetweenSteps = 2.0f;

    [HideInInspector]
    public List<float> angleSets = new List<float>();
    [HideInInspector]
    public List<float> stepRotations = new List<float>();

    //control vars
    private int currentStep = 0; //current step in rotations

    private float timeLastStep = 0.0f; //time of last step change
    private float startingRotation = 0.0f; //the starting rotation

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        timeLastStep = Time.time;
        startingRotation = transform.rotation.eulerAngles.y;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if(Time.time > timeLastStep + timeBetweenSteps)
            {
                Turn();
            }
            if(Time.time > startTime + lifeTime)
            {
                Destroy(gameObject);
            }
            myRigid.velocity = transform.forward * travelSpeed;
        }
	}

    //setup vars
    public void SetupVars(float speed, List<float> angles, float stepTime)
    {
        travelSpeed = speed;
        numSteps = angles.Count;
        timeBetweenSteps = stepTime;
        //add the angles to list
        for (int i = 0; i < angles.Count; i++)
        {
            angleSets.Add(angles[i]);
        }
        SetupRotations();
    }

    public void SetupVars(float speed, List<float> angles, float stepTime, float life)
    {
        travelSpeed = speed;
        numSteps = angles.Count;
        lifeTime = life;
        timeBetweenSteps = stepTime;
        //add the angles to list
        for (int i = 0; i < angles.Count; i++)
        {
            angleSets.Add(angles[i]);
        }
        SetupRotations();
    }

    //setup the rotations to use
    private void SetupRotations()
    {
        for(int i = 0; i < angleSets.Count; i++)
        {
            stepRotations.Add(startingRotation + angleSets[i]);
        }
    }

    //turn logic
    private void Turn()
    {
        //set timing
        timeLastStep = Time.time;
        //increment the step
        currentStep++;
        //if current step exceeds list size
        if(currentStep >= stepRotations.Count)
        {
            //reset
            currentStep = 0;
        }
        //set rotation to current step
        transform.rotation = Quaternion.Euler(0.0f, stepRotations[currentStep], 0.0f);
    }
}
