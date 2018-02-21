using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OutlawBehaviour : MonoBehaviour {

    [Header("Attack vars")]
    [Tooltip("The bullet game object enemy fires")]
    public GameObject bulletObject;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed = 5.0f;
    [Tooltip("Amount of damage this unit is doing")]
    public float damage = 1.0f;
    [Tooltip("Number of shots in round")]
    public int numShotsInRound = 3;

    [Header("Timing vars")]
    [Tooltip("Time between shots in round")]
    public float timeBetweenShots = 0.1f;
    [Tooltip("Time between rounds")]
    public float timeBetweenRounds = 1.0f;

    [Header("Tags")]
    public string targetTag = "Player";

    //set up vars
    [HideInInspector]
    public float setupDistance = 0.0f;
    [HideInInspector]
    public float setupTime = 0.0f;

    //control vars
    private int currentShotInRound = 0; //the current shot in a round
    private float timeTillNextShot = 0.0f; //the time till the next shot should be fired
    private float timeLastShot = 0.0f; //the time the last shot was fired
    private float pauseStartTime = 0.0f; //the time pause was called
    private float pauseEndTime = 0.0f; //the time end pause was called
    private float tempSetupTime = 0.0f; //setup time recalculated when coming out of pause
    private float setupStartTime = 0.0f; //time setup began
    private bool isSetup = false; //check if setup has been completed
    [HideInInspector]
    public bool isMoving = false; //check if object is currently moving
    private bool isPaused = false; //checks if pause has been called

    private Vector3 locationToSetup = Vector3.zero;
    private GameObject target; //the target this object is attacking

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            //check if setup has finished
            if (isSetup)
            {
                AttackBehaviour();
            }
            else if (transform.position == locationToSetup)
            {
                isSetup = true;
                isMoving = false;
            }
        }

	}

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //setup vars
    public void SetupVars(float setupDist, float time)
    {
        setupDistance = setupDist;
        setupTime = time;
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    //move
    public void MoveToSetupLocation()
    {
        //get the location to setup to
        locationToSetup = transform.position + (transform.forward * setupDistance);
        //tween to location
        transform.DOMove(locationToSetup, setupTime);
        //set setup start time to now
        setupStartTime = Time.time;
    }

    //Behaviour when setup has completed
    public void AttackBehaviour()
    {
        //face the target
        transform.LookAt(target.transform.position);
        //try to attack
        Attack();
    }

    //Attack behaviour <- runs on timer
    public void Attack()
    {
        //check timing
        if(Time.time > timeLastShot + timeTillNextShot + (pauseEndTime - pauseStartTime))
        {
            //create a shot
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //setup the bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //increment the current bullet count
            currentShotInRound++;
            //set time till next shot
            if(currentShotInRound >= numShotsInRound)
            {
                timeTillNextShot = timeBetweenRounds;
                currentShotInRound = 0;
            }
            else
            {
                timeTillNextShot = timeBetweenShots;
            }
            //set time last shot to now
            timeLastShot = Time.time;
            //reset pause time
            pauseStartTime = 0.0f;
            pauseEndTime = 0.0f;
        }
    }

    //pause funcs
    void OnPause()
    {
        isPaused = true;
        pauseStartTime = Time.time;
        //if currently moving
        if (isMoving)
        {
            //kill tween
            DOTween.Kill(transform);
            //get tempoary setup time
            tempSetupTime = (setupStartTime + setupTime) - Time.time;
        }
    }

    void OnUnpause()
    {
        isPaused = false;
        pauseEndTime = Time.time;
        //if currently moving
        if (isMoving)
        {
            //resume the tween using temp setup time
            transform.DOMove(locationToSetup, tempSetupTime);
        }
    }
}
