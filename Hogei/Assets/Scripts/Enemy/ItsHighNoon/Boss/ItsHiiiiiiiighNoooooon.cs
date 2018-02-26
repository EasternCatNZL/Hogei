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
    [Tooltip("Num bullet waves phase 1")]
    public int numWavesPhaseOne = 1;

    //control vars
    private bool inPhaseOne = false; //boss phase 1
    private bool inPhaseTwo = false; //boss phase 2
    private bool inPhaseThree = false; //boss phase 3

    private bool isTongueShot = false; //Check if tongue has been launched

    private int bossTotalHealth = 0; //the total amount of health boss has
    private int bossCurrentHealth = 0; //the total amount of health the boss currently has
    private int phaseTwoStartHealth = 0; //health threshold where phase 2 begins
    private int phaseThreeStartHealth = 0; //health threshold where phase 3 begins

    private int currentShot = 0; //the current shot number

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
        //SetUpHealthValues();
        //health = GetComponent<EntityHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        ChangePhase();
        if (inPhaseOne)
        {
            PhaseOne();
        }
        else if (inPhaseTwo)
        {
            
        }
        else if (inPhaseThree)
        {

        }
        
	}

    //Setup for health values
    private void SetUpHealthValues()
    {
        bossTotalHealth = (int)GetComponent<EntityHealth>().MaxHealth;
        phaseThreeStartHealth = bossTotalHealth / 3;
        phaseTwoStartHealth = phaseThreeStartHealth * 2;
    }

    //Attack sequence based on current phase
    private void PhaseOne()
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
            else if (0 < currentShot)
            {
                //check timing
                if (Time.time > lastSprayTime + sprayDelay)
                {
                    nightBird.BulletSpray();
                    currentShot++;
                    lastSprayTime = Time.time;
                }
            }

            //check if reached max shots
            if (currentShot >= numWavesPhaseOne)
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
                print("Fella");
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
            }
        }
        if (inPhaseTwo)
        {
            if(health.CurrentHealth <= phaseThreeStartHealth)
            {
                inPhaseTwo = false;
                inPhaseThree = true;

                timePhaseThreeStart = Time.time;
            }
        }
    }
}
