﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarnPhaseHandler : MonoBehaviour {

    [Header("Health phase changes")]
    [Tooltip("Phase two start health")]
    public float phaseTwoStartHealth = 200.0f;
    [Tooltip("Phase three start health")]
    public float phaseThreeStartHealth = 100.0f;

    [Header("Object ref")]
    public GameObject barn;

    [Header("Phase transition time")]
    [Tooltip("Delay 1 to 2")]
    public float delayPhaseOneToTwo = 2.0f;

    [Header("Final phase move vars")]
    [Tooltip("How far to move down <- out of screen")]
    public float descentAmount = 15.0f;
    [Tooltip("Movement time")]
    public float moveTime = 5.0f;
    [Tooltip("The transform where rise begins")]
    public Transform riseLocation;
    [Tooltip("The transform where rise ends")]
    public Transform riseEndLocation;


    [Header("Script refs")]
    public EntityHealth health;
    public BarnAxisMovement axisMove;
    public BarnSheepLauncher sheepLaunch;
    public BarnSheepRumble sheepRumble;

    [Header("Cannon refs")]
    public BarnCannonHandler[] cannonArray = new BarnCannonHandler[0];

    //control vars
    private bool inPhaseOne = true; //checks in phase one
    private bool inPhaseTwo = false; //checks in phase two
    private bool inPhaseThree = false; //checks in phase three

    private bool isMoving = false; //checks if in descent for final phase

    private float barnSinkStartTime = 0.0f; //time when barn desent began

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangePhase();
	}

    //change phase
    private void ChangePhase()
    {
        if (inPhaseOne)
        {
            //if health condition reached, change
            if (health.CurrentHealth <= phaseTwoStartHealth)
            {
                //change phase
                inPhaseOne = false;
                inPhaseTwo = true;
                //set take damage to off <- invunerable

                //start phase two
                Invoke("StartPhaseTwo", delayPhaseOneToTwo);
            }
        }
        else if (inPhaseTwo)
        {
            if (health.CurrentHealth <= phaseThreeStartHealth)
            {
                //change phase
                inPhaseTwo = false;
                inPhaseThree = true;
                //turn off movement
                axisMove.doRotation = false;
                //turn off sheep launcher
                sheepLaunch.isUsing = false;
                //turn off cannons
                for (int i = 0; i < cannonArray.Length; i++)
                {
                    cannonArray[i].isUsing = false;
                }
            }
        }
    }

    //start phase 2
    private void StartPhaseTwo()
    {
        //start using sheep launcher
        sheepLaunch.isUsing = true;
        //set take damage to on

    }

    //logic to move into final phase
    private void SinkBarn()
    {
        //set timing
        barnSinkStartTime = Time.time;
        //set moving down to true
        isMoving = true;
        //start movement sequence
        Sequence moveToFinal = DOTween.Sequence();
        //move down
        //get vector below current pos
        Vector3 belowSelf = new Vector3(transform.position.x, transform.position.y + descentAmount, transform.position.z);
        moveToFinal.Append(transform.DOMove(belowSelf, moveTime));
        Invoke("MoveToPrepLocation", moveTime);
    }

    private void MoveToPrepLocation()
    {
        //move to location
        barn.transform.position = riseLocation.position;
        barn.transform.rotation = riseLocation.rotation;
        //begin rise
        transform.DOMove(riseEndLocation.position, moveTime);
    }

    //start phase 3
    private void StartPhaseThree()
    {
        //turn on sheep rumble
        sheepRumble.isUsing = true;
    }
}