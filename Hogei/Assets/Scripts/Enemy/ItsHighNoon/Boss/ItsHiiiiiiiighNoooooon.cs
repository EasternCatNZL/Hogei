using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsHiiiiiiiighNoooooon : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 15.0f;
    [Tooltip("Prep time between phases")]
    public float prepTime = 3.0f;

    //control vars
    private bool inPhaseOne = false; //boss phase 1
    private bool inPhaseTwo = false; //boss phase 2
    private bool inPhaseThree = false; //boss phase 3

    private bool isTongueShot = false; //Check if tongue has been launched

    private int bossTotalHealth = 0; //the total amount of health boss has
    private int bossCurrentHealth = 0; //the total amount of health the boss currently has
    private int phaseTwoStartHealth = 0; //health threshold where phase 2 begins
    private int phaseThreeStartHealth = 0; //health threshold where phase 3 begins

    private float timePhaseOneStart = 0.0f; //the time phase one started
    private float timePhaseTwoStart = 0.0f; //the time phase two started
    private float timePhaseThreeStart = 0.0f; //the time phase three started

    private float timeLastAttack = 0.0f; //time last attack began
    private float tongueShotTime = 0.0f; //time tongue was shot

    //script refs
    FrogTongue tongue;
    NightBird nightBird;

	// Use this for initialization
	void Start () {
        SetUpHealthValues();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
