using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetupBullet : BulletBehavior {

    //set up vars
    [HideInInspector]
    public Vector3 setupDestination = new Vector3(0, 0, 0);
    [HideInInspector]
    public float setupTime = 0.0f;
    [HideInInspector]
    public float startDelay = 0.0f;

    //control vars
    private float setupStartTime = 0.0f;

    public bool isStarting = false;
    private bool isSettingUp = false;
    //private bool isReady = false;
    private bool isMoving = false;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (isStarting)
            {
                SetUp();
            }
            else if (isSettingUp && transform.position == setupDestination)
            {
                Move();
            }
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
    }

    //Setup vars
    public void SetupVars(Vector3 dest, float setTime, float delay, float speed, float life)
    {
        setupDestination = dest;
        setupTime = setTime;
        startDelay = delay;
        travelSpeed = speed;
        lifeTime = life;
        isStarting = true;
    }

    //func called when setup ready
    private void SetUp()
    {
        //change check vars
        isStarting = false;
        isSettingUp = true;

        //set timing
        setupStartTime = Time.time;

        transform.DOMove(setupDestination, setupTime, false);
    }

    private void Move()
    {
        //change check vars
        isSettingUp = false;
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
        else if (isMoving)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }
}
