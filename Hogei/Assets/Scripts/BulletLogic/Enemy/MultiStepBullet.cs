using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MultiStepBullet : BulletBehavior {

    [Header("Steps")]
    public int numSteps = 0;

    [HideInInspector]
    public List<float> setupDistances = new List<float>();
    [HideInInspector]
    public List<float> angleChanges = new List<float>();
    [HideInInspector]
    public float setupTime = 0.0f;
    [HideInInspector]
    public float startDelay = 0.0f;

    //control vars
    private int currentStep = 0;
    private float tempSetupTime = 0.0f;
    private float setupStartTime = 0.0f;

    public bool isStarting = false;
    private bool isMoving = false;
    private bool stepsFinished = false;

    private Vector3 currentDestination = Vector3.zero;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
        currentDestination = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (CheckArrived() && !stepsFinished)
            {
                Step();
            }
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
    }

    //sets up vars for bullet behaviour
    public void SetupVars(float setTime, float speed, int steps)
    {
        setupTime = setTime;
        travelSpeed = speed;
        numSteps = steps;
        isStarting = true;
        //Step();
    }

    //step func
    public void Step()
    {
        //manage rotation
        if (currentStep > 0 && currentStep <= numSteps)
        {
            Quaternion newRotation = new Quaternion();
            //alters rotation based own rotation + given rotation
            newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + angleChanges[currentStep - 1], 0.0f);
            transform.rotation = newRotation;
        }

        //manage movement
        //if finished steps, move straight
        if (currentStep == numSteps)
        {
            myRigid.velocity = transform.forward * travelSpeed;
            stepsFinished = true;
        }
        //move to next step
        else
        {
            //set new destination using setup distance
            currentDestination = transform.position + (transform.forward * setupDistances[currentStep]);
            //begin the movement
            transform.DOMove(currentDestination, setupTime, false);
            //get timing
            setupStartTime = Time.time;
            //increment step
            currentStep++;
        }
    }

    private bool CheckArrived()
    {
        bool hasArrived = false;
        if(transform.position == currentDestination)
        {
            hasArrived = true;
        }
        return hasArrived;
    }

    //Pause events
    protected override void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
        //if steps finished, stop movement
        if (stepsFinished)
        {
            myRigid.velocity = Vector3.zero;
        }
        //still setting up
        else
        {
            //kill tween
            DOTween.Kill(transform);
            //transform.DOMove(transform.position, 0.0f, false);
            //get tempoary setup time
            tempSetupTime = (setupStartTime + setupTime) - Time.time;
        }
    }

    protected override void OnUnpause()
    {
        pauseEndTime += Time.time;
        isPaused = false;

        //if steps finished, resume straight movement
        if (stepsFinished)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
        //still setting up
        else
        {
            //resume tween using temp setup time
            transform.DOMove(currentDestination, tempSetupTime, false);
        }
    }

}
