using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MultiStepBullet : MonoBehaviour {

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Steps")]
    public int numSteps = 0;

    //set up vars
    [HideInInspector]
    public float travelSpeed = 2.0f;
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
    private float startTime = 0.0f;
    public bool isStarting = false;
    private bool isMoving = false;
    private bool stepsFinished = false;
    private Vector3 currentDestination = Vector3.zero;

    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
        currentDestination = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (CheckArrived() && !stepsFinished)
        {
            Step();
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
}
