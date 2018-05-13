using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetingBullet : BulletBehavior {

    //set up vars
    [HideInInspector]
    public Vector3 setupDestination = new Vector3(0, 0, 0);
    [HideInInspector]
    public float setupDestinationDistance = 0.0f;
    [HideInInspector]
    public float setupTime = 0.0f;
    [HideInInspector]
    public float startDelay = 0.0f;
    [HideInInspector]
    public string targetTag = "Player";

    //control vars
    private float setupStartTime = 0.0f;

    public bool isStarting = false;
    private bool isSettingUp = false;
    private bool isReady = false;
    private bool isMoving = false;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        //check if not paused first
        if (!isPaused)
        {
            if (isStarting)
            {
                SetUp();
            }
            else if (isSettingUp && transform.position == setupDestination)
            {
                Aim();
            }
            else if (isReady && Time.time > startTime + startDelay)
            {
                Move();
            }
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
        
    }

    //sets up vars for bullet behaviour
    public void SetupVars(float dist, float setTime, float delay, float speed, string tag)
    {
        setupDestinationDistance = dist;
        setupTime = setTime;
        startDelay = delay;
        travelSpeed = speed;
        isStarting = true;
        targetTag = tag;
        //get destination
        setupDestination = transform.position + (transform.forward * setupDestinationDistance);

        //print("Variables set");
    }

    //func called when setup ready
    private void SetUp()
    {
        //change check vars
        isStarting = false;
        isSettingUp = true;
        //set timing
        setupStartTime = Time.time;

        //move
        transform.DOMove(setupDestination, setupTime, false);

        //play audio
        //bulletFireSound.Play();
    }

    private void Aim()
    {
        //change check vars
        isSettingUp = false;
        isReady = true;
        //set facing target
        transform.LookAt(GameObject.FindGameObjectWithTag(targetTag).transform.position);
        //set timing
        startTime = Time.time;
    }

    private void Move()
    {
        //change check vars
        isReady = false;
        isMoving = true;
        //start moving
        myRigid.velocity = transform.forward * travelSpeed;
    }

    //Pause events
    protected override void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
        //suspend current action and prepare required vars to resume
        if (isSettingUp)
        {
            //kill tween
            DOTween.Kill(transform);
            //transform.DOMove(transform.position, 0.0f, false);
            //alter setup time 
            setupTime = (setupStartTime + setupTime) - Time.time;
        }
        else if (isReady){
            //set pause start time, needed to adjust the delay
            pauseStartTime = Time.time;
        }
        else if (isMoving)
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    protected override void OnUnpause()
    {
        isPaused = false;
        pauseEndTime += Time.time;
        //continue suspended action
        if (isSettingUp)
        {
            SetUp();
        }
        else if (isReady)
        {
            startDelay += Time.time - pauseStartTime;
        }
        else if (isMoving)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }
}
