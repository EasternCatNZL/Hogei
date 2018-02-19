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
    private bool isSetup = false; //check if setup has been completed

    private Vector3 locationToSetup = Vector3.zero;
    private GameObject target; //the target this object is attacking

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//check if setup has finished
        if(transform.position == locationToSetup)
        {
            AttackBehaviour();
        }
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
        if(Time.time > timeLastShot + timeTillNextShot)
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
        }
    }
}
