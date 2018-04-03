using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsHiiiiiiiighNoooooon : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 15.0f;
    [Tooltip("Delay to spray")]
    public float sprayDelay = 1.0f;
    [Tooltip("Prep time between phases")]
    public float prepTime = 3.0f;

    [Header("Phase 1 vars")]
    [Tooltip("Num bullet waves phase 1")]
    public int numWavesPhaseOne = 1;

    [Header("Phase 2 vars")]
    [Tooltip("Time tongue holds")]
    public float tongueTimeTwo = 2.0f;
    [Tooltip("Num of waves")]
    public int numWavesPhaseTwo = 2;
    [Tooltip("Bullet speed")]
    public float bulletSpeedPhaseTwo = 10.0f;

    [Header("Phase 3 vars")]
    [Tooltip("Time tongue holds")]
    public float tongueTimeThree = 4.0f;
    [Tooltip("Num of waves")]
    public int numWavesPhaseThree = 3;
    [Tooltip("Num layers")]
    public int numLayersPhaseThree = 2;


    //control vars
    private bool inPhaseOne = false; //boss phase 1
    private bool inPhaseTwo = false; //boss phase 2
    private bool inPhaseThree = false; //boss phase 3

    private bool isTongueShot = false; //Check if tongue has been launched

    private int bossTotalHealth = 0; //the total amount of health boss has
    //private int bossCurrentHealth = 0; //the total amount of health the boss currently has
    private int phaseTwoStartHealth = 0; //health threshold where phase 2 begins
    private int phaseThreeStartHealth = 0; //health threshold where phase 3 begins

    private int currentShot = 0; //the current shot number
    private int numShotsThisWave = 0; //number of shots in this wave

    private float timePhaseOneStart = 0.0f; //the time phase one started
    private float timePhaseTwoStart = 0.0f; //the time phase two started
    private float timePhaseThreeStart = 0.0f; //the time phase three started

    private float timeLastAttack = 0.0f; //time last attack began
    private float tongueShotTime = 0.0f; //time tongue was shot
    private float lastSprayTime = 0.0f; //time of last spray

    //script refs
    private EntityHealth health;
    public FrogTongue tongue;
    public NightBird nightBird;

	// Use this for initialization
	void Start () {
        health = GetComponent<EntityHealth>();
        SetUpHealthValues();
        
        numShotsThisWave = numWavesPhaseOne;
        inPhaseOne = true;
	}
	
	// Update is called once per frame
	void Update () {
        ChangePhase();
        PhaseLogic();
        
	}

    //Setup for health values
    private void SetUpHealthValues()
    {
        bossTotalHealth = (int)GetComponent<EntityHealth>().MaxHealth;
        phaseThreeStartHealth = bossTotalHealth / 3;
        phaseTwoStartHealth = phaseThreeStartHealth * 2;
    }

    //phase logic

    private void PhaseLogic()
    {
        //check if in middle of attack
        if (isTongueShot)
        {
            if (currentShot == 0)
            {
                //check timing
                if (Time.time > tongueShotTime + sprayDelay)
                {
                    nightBird.BulletSpray();
                    lastSprayTime = Time.time;
                    currentShot++;
                }
            }
            //else if (!tongue.isExtending && !tongue.isRetracting)
            //{

            //}
            else if (0 < currentShot)
            {
                //check timing
                if (Time.time > lastSprayTime + sprayDelay)
                {
                    nightBird.BulletSpray();
                    currentShot++;
                    lastSprayTime = Time.time;
                    print("Other waves");
                }
            }

            //check if reached max shots
            if (currentShot >= numShotsThisWave)
            {
                isTongueShot = false;
                currentShot = 0;
            }
        }
        else
        {
            //check timing
            if (Time.time > timeLastAttack + timeBetweenAttacks)
            {
                tongue.ExtendTongueAimed();
                //set time to now
                timeLastAttack = Time.time;
                tongueShotTime = Time.time;
                //set tongue shot to true
                isTongueShot = true;
            }
        }
    }

    //Change phase
    private void ChangePhase()
    {
        //check current phase to current health
        if (inPhaseOne)
        {
            //if health condition reached, change
            if(health.CurrentHealth <= phaseTwoStartHealth)
            {
                //change phase
                inPhaseOne = false;
                inPhaseTwo = true;
                //set timing
                timePhaseTwoStart = Time.time;

                numShotsThisWave = numWavesPhaseTwo;

                //change attack values
                tongue.extendHoldTime = tongueTimeTwo;

                print("Phase two start");
            }
        }
        if (inPhaseTwo)
        {
            if(health.CurrentHealth <= phaseThreeStartHealth)
            {
                //change phase
                inPhaseTwo = false;
                inPhaseThree = true;
                //set timing
                timePhaseThreeStart = Time.time;

                numShotsThisWave = numWavesPhaseThree;

                //change attack values
                tongue.extendHoldTime = tongueTimeThree;
                nightBird.numBulletLayers = numLayersPhaseThree;
                nightBird.firstLayerBulletSpeed = bulletSpeedPhaseTwo;

                print("Phase three start");
            }
        }
    }

    ////phase logic
    //private void PhaseLogic()
    //{
    //    if (inPhaseOne)
    //    {
    //        PhaseOne();
    //    }
    //    else if (inPhaseTwo)
    //    {

    //    }
    //    else if (inPhaseThree)
    //    {

    //    }
    //}
}
